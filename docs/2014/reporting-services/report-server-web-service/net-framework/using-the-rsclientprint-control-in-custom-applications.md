---
title: "Using the RSClientPrint Control in Custom Applications | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "RSPrintClient control"
  - "print controls [Reporting Services]"
  - "custom printing [Reporting Services]"
  - "client-side printing"
ms.assetid: 8c0bdd18-8905-4e22-9774-a240fc81a8a7
author: markingmyname
ms.author: maghan
manager: kfile
---
# Using the RSClientPrint Control in Custom Applications
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] ActiveX control, **RSPrintClient**, provides client-side printing for reports viewed in HTML Viewer. It provides a **Print** dialog box so that a user can initiate a print job, preview a report, specify pages to print, and change the margins. During a client-side print operation, the report server renders the report in the Image (EMF) rendering extension and uses the print capabilities of the operating system to create the print job and send it to a printer.  
  
 Client-side printing provides a way to control and improve the quality of a printout for an HTML report by side-stepping browser print settings on the user's computer, and instead using the page dimensions, margins, header, and footer text of the report to create the print output. The print control reads property values of the report to set page size and margins.  
  
 Developers who want to enable the client-side printing feature in third-party toolbars or viewers can access the ActiveX control through the **RSClientPrint** COM object. The control can be distributed freely. The following list provides recommendations for using the control:  
  
-   Use the control to improve printing for Web-based reports. You can specify the object in any of the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)]-compatible programming languages or in script. The control is not intended for [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Windows Forms applications.  
  
-   Copy the .cab file from the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] program files and add it to your custom application code base.  
  
-   Use an \<OBJECT> tag to specify the control.  
  
-   Specify a relative or fully qualified URL to the .cab file in the OBJECT CODEBASE attribute.  
  
-   Specify your own application version information for the .cab file to track which version is used in your application.  
  
-   Review the Books Online topics about Image (EMF) rendering to understand how pages are rendered for print preview and output.  
  
## RSPrintClient Overview  
 The control displays a custom print dialog box that supports features common to other print dialog boxes, including print preview, page selections for specifying specific pages and ranges, page margins, and orientation. The control is packaged as a CAB file. The text in the **Print** dialog box is localized into all of the languages supported in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. **RSPrintClient** ActiveX control uses the Image rendering extension (EMF) to print the report. The following EMF device information is used: StartPage, EndPage, MarginBottom, MarginLeft, MarginTop, MarginRight, PageHeight, and PageWidth. Other device information settings for image rendering are not supported.  
  
### Language Support  
 The print control provides user interface text in different languages, and accepts input values calibrated to different measurement systems. The language and measurement system used are determined by the **Culture** and **UICulture** properties. Both properties accept LCID values. If you specify an LCID for a language that is a variation on a supported language, you will get the language that provides the closest match. If you specify an LCID that is not supported and for which no other LCID is a close match, you will get English (United States).  
  
## Using RSClientPrint in Code  
 The **RSClientPrint** object is used to gain access programmatically to the ActiveX control and its methods and properties. The control provides a modal dialog for print preview.  
  
### Specifying Default Values  
 You can initialize the **Print** dialog box with margin and page values of the report. By default, the **Print** dialog box is initialized with values from the report definition. You can use the defaults, or specify different values by setting properties on the object.  
  
 All dimensions are set in millimeters. Measurement conversion occurs at run time if the **Culture** and **UICulture** are set to locales that do not use metric measurements.  
  
 To understand which values are used for page dimensions and margins, you can use the **GetProperties** method to retrieve the default values:  
  
-   **PageHeight** and **PageWidth** specify the default page height and width. When the print control is launched, these property values are used to select the closest paper size available for the currently selected printer. If **PageWidth** is great than **PageHeight**, the orientation is set to Landscape. Otherwise, it is set to Portrait.  
  
-   **LeftMargin**, **RightMargin**, **TopMargin**, and **BottomMargin** are all set to 12.2 millimeters by default.  
  
 These properties are stored in the **Item** properties collection on the report server. The values are overwritten each time a report definition is updated.  
  
### RSClientPrint Properties  
  
|Property|Type|RW|Default|Description|  
|--------------|----------|--------|-------------|-----------------|  
|MarginLeft|Double|RW|report setting|Gets or sets the left margin. The default value if not set by the developer or specified in the report is 12.2 millimeters.|  
|MarginRight|Double|RW|report setting|Gets or sets the right margin. The default value if not set by the developer or specified in the report is 12.2 millimeters.|  
|MarginTop|Double|RW|report setting|Gets or sets the top margin. The default value if not set by the developer or specified in the report is 12.2 millimeters.|  
|MarginBottom|Double|RW|report setting|Gets or sets the bottom margin. The default value if not set by the developer or specified in the report is 12.2 millimeters.|  
|PageWidth|Double|RW|report setting|Gets or sets the page width. The default value if not set by the developer or the report definition is 215.9 millimeters.|  
|PageHeight|Double|RW|report setting|Gets or sets the page height. The default value if not set by the developer or the report definition is 279.4 millimeters.|  
|Culture|Int32|RW|Browser locale|Specifies the locale identifier (LCID). This value determines the unit of measurement for user input. For example, if a user types `3`, the value will be measured in millimeters if the language is French or inches if the language is English (United States). Valid values include: 1028, 1031, 1033, 1036, 1040, 1041, 1042, 2052, 3082.|  
|UICulture|String|RW|Client culture|Specifies string localization of the dialog box. Text in the Print dialog is localized into these languages: Chinese-Simplified, Chinese Traditional, English, French, German, Italian, Japanese, Korean, and Spanish. Valid values include: 1028, 1031, 1033, 1036, 1040, 1041, 1042, 2052, 3082.|  
|Authenticate|Boolean|RW|False|Specifies whether the control issues a GET command against the report server to initiate a connection for out-of-session printing.|  
  
### When to Set the Authenticate Property  
 When you print from within a browser session, you do not need to set the `Authenticate` property. Within the context of an active session, all requests from the print control to the report server are handled through the browser. The browser sets the session variables necessary for communication to the report server.  
  
 If you print out-of-session (for example, sending a report directly to a printer without first opening it), the print control must issue an HTTP `GET` request to set up the session with the report server. To issue the `GET` request, you set `Authenticate` to `True`.  
  
 You only need to issue the `GET` request if you are using Windows integrated security or Basic authentication. If you are using forms authentication, the `Authenticate` property is ignored. Your application code must set the session and authenticate the user using the custom security extension that you provide. If you are using forms authentication, be sure to set the expiration value on the authentication cookie to a value that preserves sessions for a reasonable interval. If the value is too low, users will be prompted to provide logon credentials each time the cookie expires.  
  
### CLSIDs  
 When you are running the report on-premises, you use the following CLSID values.  
  
-   41861299-EAB2-4DCC-986C-802AE12AC499  
  
-   5554DCB0-700B-498D-9B58-4E40E5814405  
  
-   60677965-AB8B-464f-9B04-4BA871A2F17F  
  
 When you are running the report in Windows Azure SQL Reporting, you use the following CLSID values.  
  
-   3DD32426-554D-48C0-A200-65D3BF880E38  
  
-   05662494-ACF9-446A-BE4C-7D3F7EA7F62F  
  
### RSPrintClient Support for the Print Method  
 The **RSClientPrint** object supports the **Print** method used to launch the Print dialog box. The **Print** method has the following arguments.  
  
|Argument|I/O|Type|Description|  
|--------------|----------|----------|-----------------|  
|ServerPath|In|String|Specifies the report server virtual directory (for example, https://adventure-works/reportserver).|  
|ReportPathParameters|In|String|Specifies the full name to the report in the report server folder namespace, including parameters. Reports are retrieved through URL access. For example: "/AdventureWorks Sample Reports/Employee Sales Summary&EmpID=1234"|  
|ReportName|In|String|The short name of the report (in the example above, the short name is Employee Sales Summary). It appears in the Print dialog box and in the print queue.|  
  
### Example  
 The following HTML example shows how to specify the .cab file, **Print** method, and properties in JavaScript:  
  
 `<BODY onload="Print()">`  
  
 `<OBJECT ID="RSClientPrint" CLASSID="CLSID: 5554DCB0-700B-498D-9B58-4E40E5814405D3" CODEBASE="<URL to the .CAB file>#Version=<your application version information>" VIEWASTEXT></OBJECT>`  
  
 `<script language="javascript">`  
  
 `function Print()`  
  
 `{`  
  
 `RSClientPrint.MarginLeft = 12.7;`  
  
 `RSClientPrint.MarginTop = 12.7;`  
  
 `RSClientPrint.MarginRight = 12.7;`  
  
 `RSClientPrint.MarginBottom = 12.7;`  
  
 `RSClientPrint.Culture = 1033;`  
  
 `RSClientPrint.UICulture = 9;`  
  
 `RSClientPrint.Print('http://localhost/rtm', '%2fEmployee_Sales_Summary&ReportMonth=6&ReportYear=2004&EmpID=20', 'Employee_Sales_Summary')`  
  
 `}`  
  
 `</script>`  
  
 `</BODY>`  
  
## See Also  
 [Print Reports from a Browser with the Print Control &#40;Report Builder and SSRS&#41;](../../report-builder/print-reports-from-a-browser-with-the-print-control-report-builder-and-ssrs.md)   
 [Print Reports &#40;Report Builder and SSRS&#41;](../../report-builder/print-reports-report-builder-and-ssrs.md)   
 [Image Device Information Settings](../../image-device-information-settings.md)  
  
  
