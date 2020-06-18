Imports System.IO
Imports System.IO.Pipes

Public Class Form1
    Private AudioFormats As New List(Of String)
    Private exhale_version As String = String.Empty
    Private ffmpeg_version As String = String.Empty
    Public Running As Boolean = False

    Private Sub InputBrowseBtn_Click(sender As Object, e As EventArgs) Handles InputBrowseBtn.Click
        Dim InputBrowser As New FolderBrowserDialog With {
            .ShowNewFolderButton = False
        }
        Dim OkAction As MsgBoxResult = InputBrowser.ShowDialog
        If OkAction = MsgBoxResult.Ok Then
            InputTxt.Text = InputBrowser.SelectedPath
        End If
    End Sub

    Private Sub OutputBrowseBtn_Click(sender As Object, e As EventArgs) Handles OutputBrowseBtn.Click
        Dim OutputBrowser As New FolderBrowserDialog With {
            .ShowNewFolderButton = True
        }
        Dim OkAction As MsgBoxResult = OutputBrowser.ShowDialog
        If OkAction = MsgBoxResult.Ok Then
            OutputTxt.Text = OutputBrowser.SelectedPath
        End If
    End Sub
    Private Sub DisableElements()
        StartBtn.Enabled = False
        InputTxt.Enabled = False
        OutputTxt.Enabled = False
        InputBrowseBtn.Enabled = False
        InputFileBtn.Enabled = False
        OutputBrowseBtn.Enabled = False
        PresetNumberBox.Enabled = False
        enableMultithreading.Enabled = False
        CPUThreads.Enabled = False
        Running = True
    End Sub
    Private Sub StartBtn_Click(sender As Object, e As EventArgs) Handles StartBtn.Click
        If InputTxt.Text = String.Empty Then
            MessageBox.Show("There was no input file Or folder specified. Cannot encode")
            Exit Sub
        End If
        If PresetNumberBox.Value < 1 Or PresetNumberBox.Value > 9 Then
            MessageBox.Show("The preset value must be a value from 1 to 9.")
            Exit Sub
        End If
        DisableElements()
        Dim StartTasks As New Threading.Thread(Sub() StartThreads())
        StartTasks.Start()
    End Sub
    Public Sub StartGoogleDriveEncodes(GDriveItemsToProcess As List(Of String), GDriveItemIDs As List(Of String))
        DisableElements()
        Dim StartTasks As New Threading.Thread(Sub() StartThreads(True, GDriveItemsToProcess, GDriveItemIDs))
        StartTasks.Start()
    End Sub

    Private Function GetOutputPath(OutputFolder As String, Item As String) As String
        Dim outputPath As String
        If Not String.IsNullOrEmpty(OutputFolder) Then
            outputPath = OutputTxt.Text + "\" + IO.Path.GetFileNameWithoutExtension(Item) + ".m4a"
        Else
            outputPath = IO.Path.ChangeExtension(Item, ".m4a")
        End If
        Return outputPath
    End Function
    Public Sub StartThreads(Optional GoogleDrive As Boolean = False, Optional GDriveItemsToProcess As List(Of String) = Nothing, Optional GDriveItemIDs As List(Of String) = Nothing)
        If Not String.IsNullOrEmpty(OutputTxt.Text) Then If Not IO.Directory.Exists(OutputTxt.Text) Then IO.Directory.CreateDirectory(OutputTxt.Text)
        Dim ItemsToProcess As List(Of String) = New List(Of String)
        Dim ItemsToDelete As List(Of String) = New List(Of String)
        Dim FileAlreadyExist As List(Of String) = New List(Of String)
        Dim ErrorList As List(Of String) = New List(Of String)
        Dim IgnoreFilesWithExtensions As String = String.Empty
        Dim Item_Type As Integer = 0
        If IO.File.Exists("ignore.txt") Then IgnoreFilesWithExtensions = My.Computer.FileSystem.ReadAllText("ignore.txt")
        If IO.Directory.Exists(InputTxt.Text) Or GoogleDrive Then
            Dim Items As Object
            If Not GoogleDrive Then
                Item_Type = 0
                Items = IO.Directory.GetFiles(InputTxt.Text)
            Else
                Item_Type = 1
                Items = GDriveItemsToProcess
            End If
            For Each File In Items
                If Not String.IsNullOrEmpty(OutputTxt.Text) Then
                    If Not IO.File.Exists(OutputTxt.Text + "\" + My.Computer.FileSystem.GetName(File)) Then
                        Dim FileFormat As String = Path.GetExtension(File)
                        If Not IgnoreFilesWithExtensions.Contains(FileFormat) And AudioFormats.Contains(FileFormat) Then
                            ItemsToProcess.Add(File)
                        Else
                            If Item_Type = 0 Then
                                My.Computer.FileSystem.CopyFile(File, OutputTxt.Text + "\" + My.Computer.FileSystem.GetName(File))
                            Else
                                GoogleDriveForm.drive.DownloadFile(GDriveItemIDs(GDriveItemsToProcess.IndexOf(File)), New IO.FileStream(OutputTxt.Text + "\" + My.Computer.FileSystem.GetName(File), IO.FileMode.CreateNew))
                            End If
                        End If
                    End If
                End If
            Next
        Else
            ItemsToProcess.Add(InputTxt.Text)
        End If
        ProgressBar1.BeginInvoke(Sub()
                                     ProgressBar1.Maximum = ItemsToProcess.Count
                                     ProgressBar1.Value = 0
                                 End Sub)
        Dim tasks = New List(Of Action)
        If enableMultithreading.Checked Then
            For Counter As Integer = 0 To ItemsToProcess.Count - 1
                Dim args As Array
                If GoogleDrive Then
                    args = {GDriveItemIDs(Counter), 1, GetOutputPath(OutputTxt.Text, ItemsToProcess(Counter)), IO.Path.GetExtension(ItemsToProcess(Counter)), My.Settings.Preset}
                Else
                    args = {ItemsToProcess(Counter), 0, GetOutputPath(OutputTxt.Text, ItemsToProcess(Counter)), IO.Path.GetExtension(ItemsToProcess(Counter)), My.Settings.Preset}
                End If
                If Not File.Exists(args(2)) Then
                    tasks.Add(Function() Run_exhale(args))
                Else
                    FileAlreadyExist.Add(args(2))
                End If
            Next
            Parallel.Invoke(New ParallelOptions With {.MaxDegreeOfParallelism = CPUThreads.Value}, tasks.ToArray())
        Else
            For Counter As Integer = 0 To ItemsToProcess.Count - 1
                Dim args As Array
                If GoogleDrive Then
                    args = {GDriveItemIDs(Counter), 1, GetOutputPath(OutputTxt.Text, ItemsToProcess(Counter)), My.Settings.Preset}
                Else
                    args = {ItemsToProcess(Counter), 0, GetOutputPath(OutputTxt.Text, ItemsToProcess(Counter)), My.Settings.Preset}
                End If
                If Not File.Exists(args(2)) Then
                    Run_exhale(args)
                Else
                    FileAlreadyExist.Add(args(2))
                End If
            Next
        End If
        If ItemsToDelete.Count > 0 Then
            For Each item As String In ItemsToDelete
                My.Computer.FileSystem.DeleteFile(item)
            Next
        End If
        Running = False
        StartBtn.BeginInvoke(Sub()
                                 StartBtn.Enabled = True
                                 PresetNumberBox.Enabled = True
                                 enableMultithreading.Enabled = True
                                 CPUThreads.Enabled = True
                                 InputTxt.Enabled = True
                                 OutputTxt.Enabled = True
                                 InputBrowseBtn.Enabled = True
                                 InputFileBtn.Enabled = True
                                 OutputBrowseBtn.Enabled = True
                             End Sub)
        Dim MessageToShow As String = "Finished!"
        If FileAlreadyExist.Count > 0 Then
            MessageToShow += Environment.NewLine + Environment.NewLine + "The following file(s) could Not be encoded because there's an output file with the same filename at the destination folder:" + Environment.NewLine
            For Each item As String In FileAlreadyExist
                MessageToShow += "- " + item + Environment.NewLine
            Next
        End If
        If ErrorList.Count > 0 Then
            MessageToShow += Environment.NewLine + Environment.NewLine + "The following file(s) could not be encoded. This could happen if you tried to encode non-WAV files and you don't have ffmpeg in your system:" + Environment.NewLine
            For Each item As String In FileAlreadyExist
                MessageToShow += "- " + item + Environment.NewLine
            Next
        End If
        MsgBox(MessageToShow)
    End Sub
    Private Function Download_Files(id As String) As IO.MemoryStream
        Using memoryStream As New IO.MemoryStream
            GoogleDriveForm.drive.DownloadFile(id, memoryStream)
            Return memoryStream
        End Using
    End Function
    Private Function Download_Files(id As String, filename As String) As Boolean
        GoogleDriveForm.drive.DownloadFile(id, filename)
        Return True
    End Function
    Private Function Run_exhale(args As Array) As Boolean
        Dim Input_File As String = args(0)
        Dim Input_Type As Integer = args(1)
        Dim Output_File As String = args(2)
        Dim FileExtension As String = args(3)
        Dim Preset As String = args(4)
        Dim exhaleProcessInfo As New ProcessStartInfo
        Dim exhaleProcess As Process
        Dim TempMetadataFileName As String = Path.GetTempFileName
        exhaleProcessInfo.FileName = "exhale.exe"
        Dim Data As Byte()
        If Input_Type = 0 Then
            Data = IO.File.ReadAllBytes(Input_File)
        Else
            Data = Download_Files(Input_File).ToArray()
        End If
        If Not ffmpeg_version = String.Empty And Not FileExtension = ".wav" Then
            Data = ffmpeg_preprocess(Data, Input_File, TempMetadataFileName)
        ElseIf Not FileExtension = ".wav" Then
            Return False
        End If
        Dim TempOutputFile As String = Path.GetTempFileName
        File.Delete(TempOutputFile)
        exhaleProcessInfo.Arguments = Preset + " """ + TempOutputFile + """"
        exhaleProcessInfo.WorkingDirectory = Path.GetDirectoryName(Input_File)
        exhaleProcessInfo.CreateNoWindow = True
        exhaleProcessInfo.RedirectStandardOutput = True
        exhaleProcessInfo.RedirectStandardError = True
        exhaleProcessInfo.RedirectStandardInput = True
        exhaleProcessInfo.UseShellExecute = False
        exhaleProcess = Process.Start(exhaleProcessInfo)
        Dim ffmpegIn As IO.Stream = exhaleProcess.StandardInput.BaseStream
        ffmpegIn.Write(Data, 0, Data.Length)
        ffmpegIn.Flush()
        ffmpegIn.Close()
        Dim errorOutput As String = exhaleProcess.StandardError.ReadToEnd
        exhaleProcess.WaitForExit()
        If Not ffmpeg_version = String.Empty Then
            ffmpeg_apply_tags(TempOutputFile, Output_File, TempMetadataFileName)
        Else
            File.Copy(TempOutputFile, Output_File)
        End If
        If File.Exists(TempMetadataFileName) Then File.Delete(TempMetadataFileName)
        If File.Exists(TempOutputFile) Then File.Delete(TempOutputFile)
        ProgressBar1.BeginInvoke(Sub() ProgressBar1.PerformStep())
        Return True
    End Function
    Private Function ffmpeg_preprocess(Input As Byte(), Filename As String, MetadataFile As String) As Byte()
        Dim input_file As String = IO.Path.GetFileName(Filename)
        Dim output_file As String = IO.Path.GetFileNameWithoutExtension(Filename) + ".wav"
        Dim InputPipe As New NamedPipeServerStream(input_file, PipeDirection.Out, -1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 16384, 0)
        Dim OutputPipe As New NamedPipeServerStream(output_file, PipeDirection.In, -1, PipeTransmissionMode.Byte, PipeOptions.WriteThrough, 0, 16384)
        Dim ffmpegProcessInfo As New ProcessStartInfo
        Dim ffmpegProcess As Process
        ffmpegProcessInfo.FileName = "ffmpeg.exe"
        ffmpegProcessInfo.Arguments = "-i ""\\.\pipe\" + input_file + """ -f ffmetadata """ + MetadataFile + """ -f wav -bitexact -map_metadata -1 ""\\.\pipe\" + output_file + """ -y"
        ffmpegProcessInfo.CreateNoWindow = True
        ffmpegProcessInfo.RedirectStandardInput = True
        ffmpegProcessInfo.RedirectStandardOutput = True
        ffmpegProcessInfo.UseShellExecute = False
        ffmpegProcess = Process.Start(ffmpegProcessInfo)
        WriteByteAsync(InputPipe, Input)
        Dim lastRead As Integer
        OutputPipe.WaitForConnection()
        Dim PipedOutput As Byte()
        Using ms As New IO.MemoryStream
            Dim buffer As Byte() = New Byte(16384) {}
            Do
                lastRead = OutputPipe.Read(buffer, 0, 16384)
                ms.Write(buffer, 0, lastRead)
            Loop While lastRead > 0
            PipedOutput = ms.ToArray()
            OutputPipe.Close()
        End Using
        ffmpegProcess.WaitForExit()
        Return PipedOutput
    End Function
    Private Function ffmpeg_apply_tags(Input As String, Output As String, MetadataFile As String) As Boolean
        Dim ffmpegProcessInfo As New ProcessStartInfo
        Dim ffmpegProcess As Process
        ffmpegProcessInfo.FileName = "ffmpeg.exe"
        ffmpegProcessInfo.Arguments = "-i """ + Input + """ -f ffmetadata -i """ + MetadataFile + """ -c:a copy -map_metadata 1 """ + Output + """ -y"
        ffmpegProcessInfo.CreateNoWindow = True
        ffmpegProcessInfo.RedirectStandardInput = False
        ffmpegProcessInfo.RedirectStandardOutput = False
        ffmpegProcessInfo.UseShellExecute = False
        ffmpegProcess = Process.Start(ffmpegProcessInfo)
        ffmpegProcess.WaitForExit()
        Return True
    End Function
    Private Async Sub WriteByteAsync(InputPipe As NamedPipeServerStream, Input As Byte())
        InputPipe.WaitForConnection()
        Dim ChunkSize As Integer = 16384
        For Bytes As Long = 0 To Input.Length Step 16384
            Try
                If Input.Length - Bytes < ChunkSize Then
                    ChunkSize = Input.Length - Bytes
                End If
                Await InputPipe.WriteAsync(Input, Bytes, ChunkSize)
            Catch
            End Try
        Next
        Try
            InputPipe.Flush()
            InputPipe.Dispose()
        Catch
        End Try
    End Sub
    Private Async Sub WriteByteAsync(InputPipe As FileStream, Input As Byte())
        Dim ChunkSize As Integer = 16384
        For Bytes As Long = 0 To Input.Length Step 16384
            Try
                If Input.Length - Bytes < ChunkSize Then
                    ChunkSize = Input.Length - Bytes
                End If
                Await InputPipe.WriteAsync(Input, Bytes, ChunkSize)
            Catch
            End Try
        Next
    End Sub
    Private Sub TrimAudioFormats(AudioFormatsList As IEnumerable(Of String))
        For Each Format As String In AudioFormatsList
            AudioFormats(AudioFormats.IndexOf(Format)) = Format.Trim
        Next
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If IO.File.Exists("audioformats.txt") Then
            AudioFormats.AddRange(IO.File.ReadAllText("audioformats.txt").Trim.Split(Environment.NewLine))
            For item As Integer = 0 To AudioFormats.Count - 1
                AudioFormats(item) = AudioFormats(item).Trim
            Next
        Else
            AudioFormats.AddRange({".flac", ".wav", ".mp3", ".mp4", ".m4a", ".ogg", ".opus", ".wma"})
        End If
        CPUThreads.Maximum = Environment.ProcessorCount
        If My.Settings.CPUThreads = 0 Then CPUThreads.Value = CPUThreads.Maximum Else CPUThreads.Value = My.Settings.CPUThreads
        PresetNumberBox.Value = My.Settings.Preset
        enableMultithreading.Checked = My.Settings.Multithreading
        IO.Directory.SetCurrentDirectory(IO.Path.GetDirectoryName(Process.GetCurrentProcess.MainModule.FileName))
        GetExchaleVersion()
        GetFFmpegVersion()
        Dim vars As String() = Environment.GetCommandLineArgs
        If vars.Count > 1 Then
            InputTxt.Text = vars(1)
        End If
    End Sub

    Private Sub GetExchaleVersion()
        Try
            Dim exhaleProcessInfo As New ProcessStartInfo
            Dim exhaleProcess As Process
            exhaleProcessInfo.FileName = "exhale.exe"
            exhaleProcessInfo.Arguments = "-V"
            exhaleProcessInfo.CreateNoWindow = True
            exhaleProcessInfo.RedirectStandardOutput = True
            exhaleProcessInfo.UseShellExecute = False
            exhaleProcess = Process.Start(exhaleProcessInfo)
            exhaleProcess.WaitForExit()
            exhale_version = exhaleProcess.StandardOutput.ReadLine()
            exhaleVersionLabel.Text = "exhale version: " + exhale_version
        Catch ex As Exception
            MessageBox.Show("exhale.exe was not found. Exiting...")
            Me.Close()
        End Try
    End Sub
    Private Sub GetFFmpegVersion()
        Try
            Dim ffmpegProcessInfo As New ProcessStartInfo
            Dim ffmpegProcess As Process
            ffmpegProcessInfo.FileName = "ffmpeg.exe"
            ffmpegProcessInfo.CreateNoWindow = True
            ffmpegProcessInfo.RedirectStandardError = True
            ffmpegProcessInfo.UseShellExecute = False
            ffmpegProcess = Process.Start(ffmpegProcessInfo)
            ffmpegProcess.WaitForExit()
            ffmpeg_version = ffmpegProcess.StandardError.ReadLine()
            ffmpegVersionLabel.Text = ffmpeg_version
        Catch ex As Exception
            ffmpegVersionLabel.Text = "ffmpeg.exe was not found. Encoding of non-WAV files will not be possible."
        End Try
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles PresetNumberBox.ValueChanged
        My.Settings.Preset = PresetNumberBox.Value
        My.Settings.Save()
    End Sub

    Private Sub EnableMultithreading_CheckedChanged(sender As Object, e As EventArgs) Handles enableMultithreading.CheckedChanged
        My.Settings.Multithreading = enableMultithreading.Checked
        My.Settings.Save()
    End Sub
    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles MyBase.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles MyBase.DragDrop
        If InputTxt.Enabled Then
            InputTxt.Text = CType(e.Data.GetData(DataFormats.FileDrop), String())(0)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles InputFileBtn.Click
        Dim InputBrowser As New OpenFileDialog With {
          .Title = "Browse for a music file:",
          .FileName = ""
      }
        Dim OkAction As MsgBoxResult = InputBrowser.ShowDialog
        If OkAction = MsgBoxResult.Ok Then
            InputTxt.Text = InputBrowser.FileName
        End If
    End Sub

    Private Sub FfmpegVersionLabel_Click(sender As Object, e As EventArgs) Handles ffmpegVersionLabel.Click
        If Not ffmpeg_version = String.Empty Then
            Clipboard.SetText(ffmpeg_version)
        End If
    End Sub

    Private Sub exhaleVersionLabel_Click(sender As Object, e As EventArgs) Handles exhaleVersionLabel.Click
        If Not exhale_version = String.Empty Then
            Clipboard.SetText(exhale_version)
        End If
    End Sub

    Private Sub GoogleDriveButton_Click(sender As Object, e As EventArgs) Handles GoogleDriveButton.Click
        Dim gdriveForm As New GoogleDriveForm With {.Owner = Me}
        gdriveForm.Show()
    End Sub

    Private Sub CPUThreads_ValueChanged(sender As Object, e As EventArgs) Handles CPUThreads.ValueChanged
        My.Settings.CPUThreads = CPUThreads.Value
        My.Settings.Save()
    End Sub
End Class
