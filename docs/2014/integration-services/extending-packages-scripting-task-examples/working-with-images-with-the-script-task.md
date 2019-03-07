---
title: "Working with Images with the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "graphics [Integration Services]"
  - "Script task [Integration Services], images"
  - "Drawing namespace"
  - "images [Integration Services]"
  - "SSIS Script task, images"
  - "Script task [Integration Services], examples"
  - "thumbnails [Integration Services]"
  - "System.Drawing namespace"
  - "JPEG format [Integration Services]"
  - ".jpeg files"
ms.assetid: 74aeb7ab-51b2-4b9f-84ee-0b46a7908ab9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Working with Images with the Script Task
  A database of products or users frequently includes images in addition to text and numeric data. The `System.Drawing` namespace in the Microsoft .NET Framework provides classes for manipulating images.  
  
 [Example 1: Convert Images to JPEG Format](#example1)  
  
 [Example 2: Create and Save Thumbnail Images](#example2)  
  
> [!NOTE]  
>  If you want to create a task that you can more easily reuse across multiple packages, consider using the code in this Script task sample as the starting point for a custom task. For more information, see [Developing a Custom Task](../extending-packages-custom-objects/task/developing-a-custom-task.md).  
  
##  <a name="example1"></a> Example 1 Description: Convert Images to JPEG Format  
 The following example opens an image file specified by a variable and saves it as a compressed JPEG file by using an encoder. The code to retrieve encoder information is encapsulated in a private function.  
  
#### To configure this Script Task example for use with a single image file  
  
1.  Create a string variable named `CurrentImageFile` and set its value to the path and file name of an existing image file.  
  
2.  On the **Script** page of the **Script Task Editor**, add the `CurrentImageFile` variable to the `ReadOnlyVariables` property.  
  
3.  In the script project, set a reference to the `System.Drawing` namespace.  
  
4.  In your code, use `Imports` statements to import the `System.Drawing` and `System.IO` namespaces.  
  
#### To configure this Script Task example for use with multiple image files  
  
1.  Place the Script task within a Foreach Loop container.  
  
2.  On the **Collection** page of the **Foreach Loop Editor**, select the **Foreach File Enumerator** as the enumerator, and specify the path and file mask of the source files, such as "*.bmp."  
  
3.  On the **Variable Mappings** page, map the `CurrentImageFile` variable to Index 0. This variable passes the current file name to the Script task on each iteration of the enumerator.  
  
    > [!NOTE]  
    >  These steps are in addition to the steps listed in the procedure for use with a single image file.  
  
### Example 1 Code  
  
```vb  
Public Sub Main()  
  
    'Create and initialize variables.  
    Dim currentFile As String  
    Dim newFile As String  
    Dim bmp As Bitmap  
    Dim eps As New Imaging.EncoderParameters(1)  
    Dim ici As Imaging.ImageCodecInfo  
    Dim supportedExtensions() As String = _  
        {".BMP", ".GIF", ".JPG", ".JPEG", ".EXIF", ".PNG", _  
        ".TIFF", ".TIF", ".ICO", ".ICON"}  
  
    Try  
        'Store the variable in a string for local manipulation.  
        currentFile = Dts.Variables("CurrentImageFile").Value.ToString  
        'Check the extension of the file against a list of  
        'files that the Bitmap class supports.  
        If Array.IndexOf(supportedExtensions, _  
            Path.GetExtension(currentFile).ToUpper) > -1 Then  
  
            'Load the file.  
            bmp = New Bitmap(currentFile)  
  
            'Calculate the new name for the compressed image.  
            'Note: This will overwrite existing JPEGs.  
            newFile = Path.Combine( _  
                Path.GetDirectoryName(currentFile), _  
                String.Concat(Path.GetFileNameWithoutExtension(currentFile), _  
                ".jpg"))  
  
            'Specify the compression ratio (0=worst quality, 100=best quality).  
            eps.Param(0) = New Imaging.EncoderParameter( _  
                Imaging.Encoder.Quality, 75)  
  
            'Retrieve the ImageCodecInfo associated with the jpeg format.  
            ici = GetEncoderInfo("image/jpeg")  
  
            'Save the file, compressing it into the jpeg encoding.  
            bmp.Save(newFile, ici, eps)  
        Else  
            'The file is not supported by the Bitmap class.  
            Dts.Events.FireWarning(0, "Image Resampling Sample", _  
                "File " & currentFile & " is not a supported format.", _  
                "", 0)  
         End If  
        Dts.TaskResult = ScriptResults.Success  
    Catch ex As Exception  
        'An error occurred.  
        Dts.Events.FireError(0, "Image Resampling Sample", _  
            ex.Message & ControlChars.CrLf & ex.StackTrace, _  
            String.Empty, 0)  
        Dts.TaskResult = ScriptResults.Failure  
    End Try  
  
End Sub  
  
Private Function GetEncoderInfo(ByVal mimeType As String) As Imaging.ImageCodecInfo  
  
    'The available image codecs are returned as an array,  
    'which requires code to iterate until the specified codec is found.  
  
    Dim count As Integer  
    Dim encoders() As Imaging.ImageCodecInfo  
  
    encoders = Imaging.ImageCodecInfo.GetImageEncoders()  
  
    For count = 0 To encoders.Length  
        If encoders(count).MimeType = mimeType Then  
            Return encoders(count)  
        End If  
    Next  
  
    'This point is only reached if a codec is not found.  
    Err.Raise(513, "Image Resampling Sample", String.Format( _  
        "The {0} codec is not available. Unable to compress file.", _  
            mimeType))  
    Return Nothing  
  
End Function  
  
```  
  
##  <a name="example2"></a> Example 2 Description: Create and Save Thumbnail Images  
 The following example opens an image file specified by a variable, creates a thumbnail of the image while maintaining a constant aspect ratio, and saves the thumbnail with a modified file name. The code that calculates the height and width of the thumbnail while maintaining a constant aspect ratio is encapsulated in a private subroutine.  
  
#### To configure this Script Task example for use with a single image file  
  
1.  Create a string variable named `CurrentImageFile` and set its value to the path and file name of an existing image file.  
  
2.  Also create the `MaxThumbSize` integer variable and assign a value in pixels, such as 100.  
  
3.  On the **Script** page of the **Script Task Editor**, add both variables to the `ReadOnlyVariables` property.  
  
4.  In the script project, set a reference to the `System.Drawing` namespace.  
  
5.  In your code, use `Imports` statements to import the `System.Drawing` and `System.IO` namespaces.  
  
#### To configure this Script Task example for use with multiple image files  
  
1.  Place the Script task within a Foreach Loop container.  
  
2.  On the **Collection** page of the **Foreach Loop Editor**, select the **Foreach File Enumerator** as the **Enumerator**, and specify the path and file mask of the source files, such as "*.jpg."  
  
3.  On the **Variable Mappings** page, map the `CurrentImageFile` variable to Index 0. This variable passes the current file name to the Script task on each iteration of the enumerator.  
  
    > [!NOTE]  
    >  These steps are in addition to the steps listed in the procedure for use with a single image file.  
  
### Example 2 Code  
  
```vb  
Public Sub Main()  
  
    Dim currentImageFile As String  
    Dim currentImage As Image  
    Dim maxThumbSize As Integer  
    Dim thumbnailImage As Image  
    Dim thumbnailFile As String  
    Dim thumbnailHeight As Integer  
    Dim thumbnailWidth As Integer  
  
    currentImageFile = Dts.Variables("CurrentImageFile").Value.ToString  
    thumbnailFile = Path.Combine( _  
        Path.GetDirectoryName(currentImageFile), _  
        String.Concat(Path.GetFileNameWithoutExtension(currentImageFile), _  
        "_thumbnail.jpg"))  
  
    Try  
        currentImage = Image.FromFile(currentImageFile)  
  
        maxThumbSize = CType(Dts.Variables("MaxThumbSize").Value, Integer)  
        CalculateThumbnailSize( _  
            maxThumbSize, currentImage, thumbnailWidth, thumbnailHeight)  
  
        thumbnailImage = currentImage.GetThumbnailImage( _  
           thumbnailWidth, thumbnailHeight, Nothing, Nothing)  
        thumbnailImage.Save(thumbnailFile)  
        Dts.TaskResult = ScriptResults.Success  
    Catch ex As Exception  
        Dts.Events.FireError(0, "Script Task Example", _  
        ex.Message & ControlChars.CrLf & ex.StackTrace, _  
        String.Empty, 0)  
        Dts.TaskResult = ScriptResults.Failure  
    End Try  
  
End Sub  
  
Private Sub CalculateThumbnailSize( _  
    ByVal maxSize As Integer, ByVal sourceImage As Image, _  
    ByRef thumbWidth As Integer, ByRef thumbHeight As Integer)  
  
    If sourceImage.Width > sourceImage.Height Then  
        thumbWidth = maxSize  
        thumbHeight = CInt((maxSize / sourceImage.Width) * sourceImage.Height)  
    Else  
        thumbHeight = maxSize  
        thumbWidth = CInt((maxSize / sourceImage.Height) * sourceImage.Width)  
    End If  
  
End Sub  
```  
  
```csharp  
bool ThumbnailCallback()  
        {  
            return false;  
        }  
        public void Main()  
        {  
  
            string currentImageFile;  
            Image currentImage;  
            int maxThumbSize;  
            Image thumbnailImage;  
            string thumbnailFile;  
            int thumbnailHeight = 0;  
            int thumbnailWidth = 0;  
  
            currentImageFile = Dts.Variables["CurrentImageFile"].Value.ToString();  
            thumbnailFile = Path.Combine(Path.GetDirectoryName(currentImageFile), String.Concat(Path.GetFileNameWithoutExtension(currentImageFile), "_thumbnail.jpg"));  
  
            try  
            {  
  
                currentImage = Image.FromFile(currentImageFile);  
  
                maxThumbSize = (int)Dts.Variables["MaxThumbSize"].Value;  
                CalculateThumbnailSize(maxThumbSize, currentImage, ref thumbnailWidth, ref thumbnailHeight);  
  
                Image.GetThumbnailImageAbort myCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);  
  
                thumbnailImage = currentImage.GetThumbnailImage(thumbnailWidth, thumbnailHeight, ThumbnailCallback, IntPtr.Zero);  
                thumbnailImage.Save(thumbnailFile);  
                Dts.TaskResult = (int)ScriptResults.Success;  
            }  
            catch (Exception ex)  
            {  
                Dts.Events.FireError(0, "Script Task Example", ex.Message + "\r" + ex.StackTrace, String.Empty, 0);  
                Dts.TaskResult = (int)ScriptResults.Failure;  
            }  
  
        }  
  
        private void CalculateThumbnailSize(int maxSize, Image sourceImage, ref int thumbWidth, ref int thumbHeight)  
        {  
  
            if (sourceImage.Width > sourceImage.Height)  
            {  
                thumbWidth = maxSize;  
                thumbHeight = (int)(sourceImage.Height * maxSize / sourceImage.Width);  
            }  
            else  
            {  
                thumbHeight = maxSize;  
                thumbWidth = (int)(sourceImage.Width * maxSize / sourceImage.Height);  
  
            }  
  
        }  
  
```  
  
![Integration Services icon (small)](../media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
  
