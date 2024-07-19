---
title: "URL access parameter reference"
description: Learn how you can use parameters as part of a URL to configure the look and feel of your SQL Server Reporting Services (SSRS) reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/19/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "reports [Reporting Services], display options"
  - "URL access [Reporting Services], report display parameters"
# customer-intent: 
---
# URL access parameter reference

You can use parameters as part of a URL to configure the look and feel of your [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] reports. This article describes the most commonly used parameters. 

Parameters are case-insensitive and they need a specific prefix depending on what you want to modify:

- `rs:`: Targets the report server.
- `rc:`: Targets an HTML Viewer.
- `rv:`: Targets the Report Viewer web part.

You can also specify parameters that are specific to devices or rendering extensions. For more information about device-specific parameters, see [Specify device information settings in a URL](../reporting-services/specify-device-information-settings-in-a-url.md).
  
> [!IMPORTANT]  
>  For a SharePoint mode report server it's important that the URL includes the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds context to the HTTP request that's required to ensure proper execution of the report for SharePoint mode report servers. For examples, see [Access report server items by using URL access](../reporting-services/access-report-server-items-using-url-access.md).
> 
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  

##  <a name="bkmk_htmlviewer"></a> HTML Viewer commands (`rc:`)

Target the HTML Viewer by using the prefix `rc:`.

### Toolbar
  
Show or hide the toolbar. If the value of this parameter is **false**, all remaining options are ignored. If you omit this parameter, the toolbar is automatically displayed for rendering formats that support it. The default of this parameter is **true**.
  
> [!IMPORTANT]  
>  *rc:Toolbar*=**false** doesn't work for URL access strings that use an IP address, instead of a domain name, to target a report hosted on a SharePoint site.
  
### Parameters

Show or hide the parameters area of the toolbar. The default value is **true**. Valid values are:

- **True**: Displays the parameters area of the toolbar. 
- **False**: Hides the parameters area and the user can't be display them. 
- **Collapsed**: Hides the parameters area, but the user can toggle to see it.  
  
For example, in native mode:

```  
https://myrshost/reportserver?/Sales&rc:Parameters=Collapsed  
```  

For example, in SharePoint mode:
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Parameters=Collapsed  
```  
  
### Zoom

Set the report zoom value as an integer percentage or a string constant. Standard string values include **Page Width** and **Whole Page**. Versions of Internet Explorer earlier than Internet Explorer 5.0 and all non-[!INCLUDE[msCoName](../includes/msconame-md.md)] browsers ignore this parameter. The default value is **100**.
  
For example, in native mode:
  
```  
https://myrshost/reportserver?/Sales&rc:Zoom=Page Width  
```  
  
For example, in SharePoint mode:
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Zoom=Page Width  
```  
  
### Section

Set which page in the report to display. Any value that's greater than the number of pages in the report displays the last page. Any value that's less than **0** displays page 1 of the report. The default value is **1**.
  
For example, in native mode to display page 2 of the report:
  
```  
https://myrshost/reportserver?/Sales&rc:Section=2  
```  
  
For example, in SharePoint mode to display page 2 of the report:
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Section=2  
```  
  
### FindString

Search a report for a specific set of text and highlights the text.
    
> [!IMPORTANT]  
>  `rc:FindString` doesn't work unless you include `rc:Toolbar=false` to the URL access string.
  
For example, in native mode:
  
```  
https://myrshost/reportserver?/Sales&rc:Toolbar=false&rc:FindString=Mountain-400  
```  
  
For example, in SharePoint mode:
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Toolbar=false&rc:FindString=Mountain-400  
```  
  
### StartFind

Specify the last section to search. The default value is the last page of the report.  
  
An example in native mode that searches for the first occurrence of the text "Mountain-400" in the Product Catalog sample report starting with page 1 and ending with page 5:
  
```  
https://server/Reportserver?/SampleReports/Product Catalog&rs:Command=Render&rc:StartFind=1&rc:EndFind=5&rc:FindString=Mountain-400  
```  
  
### EndFind

Set the number of the last page you want to use in the search. For example, a value of **5** indicates that the last page to be searched is page 5 of the report. The default value is the number of the current page. Use this parameter with the `StartFind` parameter. For more information, see the [StartFind](#startfind) example.

### FallbackPage

Set the number of the page to display if a search or a document map selection fails. The default value is the number of the current page.
  
### GetImage

Get a particular icon for the HTML Viewer user interface.
  
### Icon

Get the icon of a particular rendering extension.

### Stylesheet

Specify a style sheet you want to apply to the HTML Viewer.
  
### Device Information Setting

Specify a device information setting in the form of `rc:tag=value`, where `tag` is the name of a device information setting specific to the rendering extension. For more information, see the [Format](#format) section. 

You can use the `OutputFormat` device information setting for the IMAGE rendering extension to render the report to a JPEG image by using the following parameters in the URL access string: `...&rs:Format=IMAGE&rc:OutputFormat=JPEG`. For more information on all extension-specific device information settings, see [Device information settings for rendering extensions &#40;Reporting Services&#41;](../reporting-services/device-information-settings-for-rendering-extensions-reporting-services.md).
  
##  <a name="bkmk_reportserver"></a> Report server commands (`rs:`)

Target the report server by using the prefix `rs:`.
  
### Command

Perform an action on a catalog item, depending on its item type. The type of the catalog item referenced in the URL access string determines the default value. Valid values are:
  
- **ListChildren** and **GetChildren**: Display the contents of a folder. The folder items display within a generic item-navigation page.
  
    For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales&rs:Command=GetChildren  
    ```  
  
    For example, a named instance in native mode:
  
    ```  
    https://myssrshost/Reportserver_THESQLINSTANCE?/reportfolder&rs:Command=listChildren  
    ```  
  
    For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rs:Command=GetChildren  
    ```  
  
- **Render**: The report renders in the browser so that you can view it.
  
    For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales/YearlySalesByCategory&rs:Command=Render  
    ```  
  
    For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/YearlySalesByCategory&rs:Command=Render  
    ```  
  
- **GetSharedDatasetDefinition**: Display the XML definition associated with a shared dataset. Shared dataset properties are saved in the definition. These properties include the query, dataset parameters, default values, dataset filters, and data options such as collation and case sensitivity. You must have **Read Report Definition** permission on a shared dataset to use this value.
  
    For example, in native mode:
  
    ```  
    https://localhost/reportserver/?/DataSet1&rs:command=GetShareddatasetDefinition  
    ```  
  
- **GetDataSourceContents**: Display the properties of a given shared data source as XML. If your browser supports XML and if you're an authenticated user with **Read Contents** permission on the data source, the data source definition displays.
  
    For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales/AdventureWorks2022&rs:Command=GetDataSourceContents  
    ```  
  
    For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/AdventureWorks2022&rs:Command=GetDataSourceContents  
    ```  
  
- **GetResourceContents**: Render a resource and displays it in an HTML page if the resource is compatible with the browser. Otherwise, you can choose to open or save the file or resource to disk.  
  
    For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales/StorePicture&rs:Command=GetResourceContents  
    ```  
  
    For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/StorePicture.jpg&rs:Command=GetResourceContents  
    ```  
  
- **GetComponentDefinition**: Display the XML definition associated with a published report item. You must have **Read Contents** permission on a published report item to use this value.
  
### Format

Specify the format in which to render and view a report. Common values include:
  
- **HTML5**    
- **PPTX**    
- **ATOM**    
- **HTML4.0**   
- **MHTML**    
- **IMAGE**    
- **EXCEL** (for .xls)    
- **EXCELOPENXML** (for .xlsx)  
- **WORD** (for .doc)    
- **WORDOPENXML** (for .docx)  
- **CSV**  
- **PDF**  
- **XML**  
  
The default value is **HTML5**. For more information, see [Export a report by using URL access](../reporting-services/export-a-report-using-url-access.md).
  
For a complete list, see the `\<Render>` extension section of the report server `rsreportserver.config` file. For more information, see [RsReportServer.config configuration file](../reporting-services/report-server/rsreportserver-config-configuration-file.md).
  
For example, to get a PDF copy of a report directly from a native mode report server:
  
```  
https://myrshost/ReportServer?/myreport&rs:Format=PDF  
```  
  
For example, to get a PDF copy of a report directly from a SharePoint mode report server:
 
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
```  
  
### ParameterLanguage

Provide a language for parameters passed in a URL that's independent of the browser language. The default value is the browser language. The value can be a culture value, such as **en-us** or **de-DE**.
  
For example, to override the browser language and specify a culture value of **de-DE** on a report server in native mode:
  
```  
https://myrshost/Reportserver?/SampleReports/Product+Line+Sales&rs:Command=Render&StartDate=4/10/2008&EndDate=11/10/2008&rs:ParameterLanguage=de-DE  
```  
  
### Snapshot

Render a report based on a report history snapshot. For more information, see [Render a report history snapshot using URL access](../reporting-services/render-a-report-history-snapshot-using-url-access.md).
  
For example, to retrieve a report history snapshot dated `2003-04-07` with a time stamp of `13:40:02` on a report server in native mode:
  
```  
https://myrshost/reportserver?/SampleReports/Company Sales&rs:Snapshot=2003-04-07T13:40:02  
```  
  
### PersistStreams

Render a report in a single persisted stream. The Image renderer uses this parameter to transmit the rendered report one chunk at a time. After using this parameter in a URL access string, use the same URL access string with the `GetNextStream` parameter instead of the `PersistStreams` parameter to get the next chunk in the persisted stream. This URL command eventually returns a 0-byte stream to indicate the end of the persisted stream. The default value is **false**.
  
### GetNextStream

Get the next data chunk in a persisted stream that's accessed by using the `PersistStreams` parameter. For more information, see the [PersistStreams](#persiststreams) description. The default value is **false**.
  
### SessionID

Specify an established active report session between the client application and the report server. The value of this parameter is set to the session identifier.
  
You can specify the session ID as a cookie or as part of the URL. When you configure the report server to not use session cookies, the first request without a specified session ID results in a redirection with a session ID. For more information about report server sessions, see [Identify execution state](../reporting-services/report-server-web-service-net-framework-soap-headers/identifying-execution-state.md).
  
### ClearSession

Remove a all report instances associated with an authenticated user from the report session. A report instance is defined as the same report run multiple times with different report parameter values. The default value is **false**. Valid values are **true** and **false**.
  
### ResetSession

Reset the report session by removing the report session's association with all report snapshots. The default value is **false**. Valid values are **true** and **false**.
  
### ShowHideToggle

Toggle the show and hide state of a section of the report. Specify a positive integer to represent the section to toggle.
  
##  <a name="bkmk_webpart"></a> Report Viewer web part commands (`rv:`)

Target the Report Viewer web part that integrates with SharePoint by using the prefix `rv:`. The Report Viewer web part also accepts the `rs:ParameterLanguage` parameter.
  
### Toolbar

Control the toolbar display for the Report Viewer web part. The default value is **Full**. Valid values are:
  
- **Full**: Displays the complete toolbar. 
- **Navigation**: Displays only pagination in the toolbar.
- **None**: Doesn't display the toolbar.
  
For example, to display only pagination in the toolbar on a report server in SharePoint mode:
  
```  
https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:Toolbar=Navigation  
```  
  
### HeaderArea

Control the header display for the Report Viewer web part. The default value is **Full**. Valid values are:
  
- **Full**: Displays the complete header.  
- **BreadCrumbsOnly**: Displays only the breadcrumb navigation in the header to inform the user where they are in the application.
- **None**: Hides the header.
  
For example, to display only the breadcrumb navigation in the header on a report server in SharePoint mode:
  
```  
https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:HeaderArea=BreadCrumbsOnly  
```  
  
### DocMapAreaWidth

Control the display width, in pixels, of the parameter area in the Report Viewer web part. The default value is the same as the Report Viewer web part default. The value must be a non-negative integer.
  
### AsyncRender

Control whether a report is rendered asynchronously. The default value is **true**, which specifies that a report be rendered asynchronously. The value must be a Boolean value of **true** or **false**.
  
### ParamMode

Control how the Report Viewer web part's parameter prompt area is displayed in full-page view. The default value is **Full**. Valid values are:
  
- **Full**: Displays the parameter prompt area. 
- **Collapsed**: Collapses the parameter prompt area.  
- **Hidden**: Hides the parameter prompt area.
  
For example, in SharePoint mode, to collapse the parameter prompt area:
  
```  
https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:ParamMode=Collapsed  
```  
  
### DocMapMode

Control how the Report Viewer web part's document map area displays in full-page view. The default value is **Full**. Valid values are:
  
- **Full**: Displays the document map area. 
- **Collapsed**: Collapses the document map area.  
- **Hidden**: Hides the document map area.
  
### DockToolBar

Control whether the Report Viewer web part's toolbar docks at the top or bottom. Valid values are **Top** and **Bottom**. The default value is **Top**.
  
For example, to dock the toolbar to the bottom on a report server in SharePoint mode:
  
```  
https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:DockToolBar=Bottom  
```  
 
### ToolBarItemsDisplayMode

Control which toolbar items to display. This value is a bitwise enumeration value. To include a toolbar item, add the item's value to the total value. For example, for no **Actions** menu, use `rv:ToolBarItemsDisplayMode=63` (or `0x3F`), which is 1+2+4+8+16+32. For **Actions** menu items only, use `rv:ToolBarItemsDisplayMode=960` (or `0x3C0`). The default value is **-1**, which includes all toolbar items. Valid values are:
 
|Value|Toolbar item|
|-----|------------|
|1 (0x1)|**Back** button|
|2 (0x2)|Text search controls|
|4 (0x4)|Page navigation controls|
|8 (0x8)|**Refresh** button|
|16 (0x10)|**Zoom** list box|
|32 (0x20)|**Atom Feed** button|
|64 (0x40)|**Print** menu option in **Actions**|
|128 (0x80)|**Export** submenu in **Actions**|
|256 (0x100)|**Open with Report Builder** menu option in **Actions**|
|512 (0x200)|**Subscribe** menu option in **Actions**|
|1024 (0x400)|**New Data Alert** menu option in **Actions**|
  
For example, in SharePoint mode to display only the **Back** button, text search controls, page navigation controls, and the **Refresh** button:
  
```  
https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:ToolBarItemsDisplayMode=15  
```  
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)
- [Export a report using URL access](../reporting-services/export-a-report-using-url-access.md)
  
  
