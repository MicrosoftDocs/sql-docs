---
title: "Using URL Access in a Windows Application | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: application-integration


ms.topic: reference
helpviewer_keywords: 
  - "Windows applications [Reporting Services]"
  - "Web Browser controls [Reporting Services]"
  - "Windows Forms [Reporting Services]"
  - "browser controls [Reporting Services]"
  - "URL access [Reporting Services], Windows applications"
ms.assetid: a4b222e5-0cbd-409c-92c4-046a674db8ac
author: maggiesMSFT
ms.author: maggies
---
# Integrating Reporting Services Using URL Access - Windows Application
  Although URL access to a report server is optimized for a Web environment, you can also use URL access to embed [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports into a [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows application. However, URL access that involves Windows Forms still requires that you use Web browser technology. You can use the following integration scenarios with URL access and Windows Forms:  
  
-   Display a report from a Windows Form application by starting a Web browser programmatically.  
  
-   Use the <xref:System.Windows.Forms.WebBrowser> control on a Windows Form to display a report.  
  
## Starting Internet Explorer from a Windows Form  
 You can use the <xref:System.Diagnostics.Process> class to access a process that is running on a computer. The <xref:System.Diagnostics.Process> class is a useful [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] construct for starting, stopping, controlling, and monitoring applications. To view a specific report in your report server database, you can start the **IExplore** process, passing in the URL to the report. The following code example can be used to start [!INCLUDE[msCoName](../../includes/msconame-md.md)] Internet Explorer and pass a specific report URL when the user clicks a button on a Windows Form.  
  
```vb  
Private Sub viewReportButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles viewReportButton.Click  
   ' Build the URL access string based on values supplied by a user  
   Dim url As String = serverUrlTextBox.Text + "?" & reportPathTextBox.Text & _  
   "&rs:Command=Render" & "&rs:Format=HTML4.0"  
  
   ' If the user does not select the toolbar check box,  
   ' turn the toolbar off in the HTML Viewer  
   If toolbarCheckBox.Checked = False Then  
      url += "&rc:Toolbar=False"  
   End If  
   ' load report in the Web browser  
   Try  
      System.Diagnostics.Process.Start("IExplore", url)  
   Catch  
      MessageBox.Show("The system could not start the specified report using Internet Explorer.", _  
      "An error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Error)  
   End Try  
End Sub 'viewReportButton_Click  
```  
  
```csharp  
// Sample click event for a Button control on a Windows Form  
private void viewReportButton_Click(object sender, System.EventArgs e)  
{  
   // Build the URL access string based on values supplied by a user  
   string url = serverUrlTextBox.Text + "?" + reportPathTextBox.Text +  
      "&rs:Command=Render" + "&rs:Format=HTML4.0";  
  
   // If the user does not check the toolbar check box,  
   // turn the toolbar off in the HTML Viewer  
   if (toolbarCheckBox.Checked == false)  
      url += "&rc:Toolbar=False";  
  
   // load report in the Web browser  
   try  
   {  
      System.Diagnostics.Process.Start("IExplore", url);  
   }  
  
   catch (Exception)  
   {  
      MessageBox.Show(  
         "The system could not open the specified report using Internet Explorer.",   
         "An error has occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);  
   }  
}  
```  
  
## Embedding a Browser Control on a Windows Form  
 If you do not want to view your report in an external Web browser, you can embed a Web browser seamlessly as part of your Windows Form using the <xref:System.Windows.Forms.WebBrowser> control.  
  
###### To add the WebBrowser control to your Windows Form  
  
1.  Create a new Windows application in either [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[csprcs](../../includes/csprcs-md.md)] or [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)].  
  
2.  Locate the <xref:System.Windows.Forms.WebBrowser> control in the **Toolbox** Dialog Box.  
  
     If the **Toolbox** is not visible you can access it by clicking the **View** menu item and selecting **Toolbox**.  
  
3.  Drag the <xref:System.Windows.Forms.WebBrowser>control onto the design surface of your Windows Form.  
  
     The <xref:System.Windows.Forms.WebBrowser>control named webBrowser1 is added to the Form  
  
 You direct the <xref:System.Windows.Forms.WebBrowser> control to a URL by calling its **Navigate** method. You can assign a specific URL access string to your <xref:System.Windows.Forms.WebBrowser> control at run time as shown in the following example.  
  
```vb  
Dim url As String = "https://localhost/reportserver?/" & _  
                    "AdventureWorks2012 Sample Reports/" & _  
                    "Company Sales&rs:Command=Render"  
WebBrowser1.Navigate(url)  
```  
  
```csharp  
string url = "https://localhost/reportserver?/" +  
             "AdventureWorks2012 Sample Reports/" +  
             "Company Sales&rs:Command=Render";  
webBrowser1.Navigate(url);  
```  
  
## See Also  
 [Integrating Reporting Services into Applications](../../reporting-services/application-integration/integrating-reporting-services-into-applications.md)   
 [Integrating Reporting Services Using URL Access](../../reporting-services/application-integration/integrating-reporting-services-using-url-access.md)   
 [Integrating Reporting Services Using SOAP](../../reporting-services/application-integration/integrating-reporting-services-using-soap.md)   
 [Integrating Reporting Services Using the ReportViewer Controls](../../reporting-services/application-integration/integrating-reporting-services-using-reportviewer-controls.md)   
 [URL Access &#40;SSRS&#41;](../../reporting-services/url-access-ssrs.md)  
  
  
