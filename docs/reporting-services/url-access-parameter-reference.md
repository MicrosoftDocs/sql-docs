---
title: "URL access parameter reference | Microsoft Docs"
description: Use the parameters in this article as part of a URL to configure the look and feel of your Reporting Services reports.
ms.date: 05/22/2020

ms.service: reporting-services
ms.subservice: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], display options"
  - "URL access [Reporting Services], report display parameters"
ms.assetid: 1c3e680a-83ea-4979-8e79-fa2337ae12a3
author: maggiesMSFT
ms.author: maggies
---
# URL access parameter reference

  You can use the following parameters as part of a URL to configure the look and feel of your [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] reports. The most common parameters are listed in this section. Parameters are case-insensitive and begin with the parameter prefix *rs:* if directed to the report server and *rc:* if directed to an HTML Viewer. You can also specify parameters that are specific to devices or rendering extensions. For more information about device-specific parameters, see [Specify device information settings in a URL](../reporting-services/specify-device-information-settings-in-a-url.md).
  
> [!IMPORTANT]  
>  For a SharePoint mode report server it's important that the URL includes the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds context to the HTTP request that's required to ensure proper execution of the report for SharePoint mode report servers. For examples, see [Access report server items using URL access](../reporting-services/access-report-server-items-using-url-access.md).
> 
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.
  

##  <a name="bkmk_htmlviewer"></a> HTML Viewer commands (rc:)
 - HTML Viewer commands are used to target the HTML Viewer and are prefixed with *rc:*:
  
-   **Toolbar**: Shows or hides the toolbar. If the value of this parameter is **false**, all remaining options are ignored. If you omit this parameter, the toolbar is automatically displayed for rendering formats that support it. The default of this parameter is **true**.
  
    > [!IMPORTANT]  
    >  *rc:Toolbar*=**false** doesn't work for URL access strings that use an IP address, instead of a domain name, to target a report hosted on a SharePoint site.
  
-   **Parameters**: Shows or hides the parameters area of the toolbar. If you set this parameter to **true**, the parameters area of the toolbar is displayed. If you set this parameter to **false**, the parameters area isn't displayed and can't be displayed by the user. If you set this parameter to a value of **Collapsed**, the parameters area won't be displayed but can be toggled by the user. The default value of this parameter is **true**.  
  
     For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Parameters=Collapsed  
    ```  
  
     For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Parameters=Collapsed  
    ```  
  
-   **Zoom**: Sets the report zoom value as an integer percentage or a string constant. Standard string values include **Page Width** and **Whole Page**. This parameter is ignored by versions of Internet Explorer earlier than Internet Explorer 5.0 and all non-[!INCLUDE[msCoName](../includes/msconame-md.md)] browsers. The default value of this parameter is **100**.
  
     For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Zoom=Page Width  
    ```  
  
     For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Zoom=Page Width  
    ```  
  
-   **Section**: Sets which page in the report to display. Any value that's greater than the number of pages in the report displays the last page. Any value that's less than **0** displays page 1 of the report. The default value of this parameter is **1**.
  
     For an example in native mode, to display page 2 of the report:
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Section=2  
    ```  
  
     For an example in SharePoint mode, to display page 2 of the report:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Section=2  
    ```  
  
-   **FindString**: Searches a report for a specific set of text and highlights the text.
    
    > [!IMPORTANT]  
    >  *rc:FindString* doesn't work unless you include *rc:Toolbar*=**false** to the URL access string.
  
     For example, in native mode:
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Toolbar=false&rc:FindString=Mountain-400  
    ```  
  
     For example, in SharePoint mode:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Toolbar=false&rc:FindString=Mountain-400  
    ```  
  
-   **StartFind**: Specifies the last section to search. The default value of this parameter is the last page of the report.  
  
     For an example in native mode that searches for the first occurrence of the text "Mountain-400" in the Product Catalog sample report starting with page 1 and ending with page 5:
  
    ```  
    https://server/Reportserver?/SampleReports/Product Catalog&rs:Command=Render&rc:StartFind=1&rc:EndFind=5&rc:FindString=Mountain-400  
    ```  
  
-   **EndFind**: Sets the number of the last page to use in the search. For example, a value of **5** indicates that the last page to be searched is page 5 of the report. The default value is the number of the current page. Use this parameter in conjunction with the *StartFind* parameter. See the previous example.
  
-   **FallbackPage**: Sets the number of the page to display if a search or a document map selection fails. The default value is the number of the current page.
  
-   **GetImage**: Gets a particular icon for the HTML Viewer user interface.
  
-   **Icon**: Gets the icon of a particular rendering extension.
  
-   **Stylesheet**: Specifies a style sheet to be applied to the HTML Viewer.
  
-   **Device Information Setting**: Specifies a device information setting in the form of `rc:tag=value`, where *tag* is the name of a device information setting specific to the rendering extension that's currently used. (See the description for the *Format* parameter.) For example, you can use the *OutputFormat* device information setting for the IMAGE rendering extension to render the report to a JPEG image by using the following parameters in the URL access string: `...&rs:Format=IMAGE&rc:OutputFormat=JPEG`. For more information on all extension-specific device information settings, see [Device information settings for rendering extensions &#40;Reporting Services&#41;](../reporting-services/device-information-settings-for-rendering-extensions-reporting-services.md).
  
##  <a name="bkmk_reportserver"></a> Report server commands (rs:)
 Report server commands are prefixed with *rs:* and are used to target the report server:
  
-   **Command**: Performs an action on a catalog item, depending on its item type. The default value is determined by the type of the catalog item referenced in the URL access string. Valid values are:
  
    -   **ListChildren** and **GetChildren**: Displays the contents of a folder. The folder items are displayed within a generic item-navigation page.
  
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
  
    -   **Render**: The report is rendered in the browser so that you can view it.
  
         For example, in native mode:
  
        ```  
        https://myrshost/reportserver?/Sales/YearlySalesByCategory&rs:Command=Render  
        ```  
  
         For example, in SharePoint mode:
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/YearlySalesByCategory&rs:Command=Render  
        ```  
  
    -   **GetSharedDatasetDefinition**: Displays the XML definition associated with a shared dataset. Shared dataset properties, including the query, dataset parameters, default values, dataset filters, and data options such as collation and case sensitivity, are saved in the definition. You must have **Read Report Definition** permission on a shared dataset to use this value.
  
         For example, in native mode:
  
        ```  
        https://localhost/reportserver/?/DataSet1&rs:command=GetShareddatasetDefinition  
        ```  
  
    -   **GetDataSourceContents**: Displays the properties of a given shared data source as XML. If your browser supports XML and if you're an authenticated user with **Read Contents** permission on the data source, the data source definition is displayed.
  
         For example, in native mode:
  
        ```  
        https://myrshost/reportserver?/Sales/AdventureWorks2012&rs:Command=GetDataSourceContents  
        ```  
  
         For example, in SharePoint mode:
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/AdventureWorks2012&rs:Command=GetDataSourceContents  
        ```  
  
    -   **GetResourceContents**: Renders a resource and displays it in an HTML page if the resource is compatible with the browser. Otherwise, you're prompted to open or save the file or resource to disk.  
  
         For example, in native mode:
  
        ```  
        https://myrshost/reportserver?/Sales/StorePicture&rs:Command=GetResourceContents  
        ```  
  
         For example, in SharePoint mode:
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/StorePicture.jpg&rs:Command=GetResourceContents  
        ```  
  
    -   **GetComponentDefinition**: Displays the XML definition associated with a published report item. You must have **Read Contents** permission on a published report item to use this value.
  
-   **Format**: Specifies the format in which to render and view a report. Common values include:
  
    -   **HTML5**  
  
    -   **PPTX**  
  
    -   **ATOM**  
  
    -   **HTML4.0**  
  
    -   **MHTML**  
  
    -   **IMAGE**  
  
    -   **EXCEL** (for .xls)
    
    -   **EXCELOPENXML** (for .xlsx)
  
    -   **WORD** (for .doc)
    
    -   **WORDOPENXML** (for .docx)
  
    -   **CSV**  
  
    -   **PDF**  
  
    -   **XML**  
  
     The default value is **HTML5**. For more information, see [Export a report using URL access](../reporting-services/export-a-report-using-url-access.md).
  
     For a complete list, see the **\<Render>** extension section of the report server rsreportserver.config file. For information on where to find the file, see [RsReportServer.config configuration file](../reporting-services/report-server/rsreportserver-config-configuration-file.md).
  
     For example, to get a PDF copy of a report directly from a native mode report server:
  
    ```  
    https://myrshost/ReportServer?/myreport&rs:Format=PDF  
    ```  
  
     For example, to get a PDF copy of a report directly from a SharePoint mode report server:
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
    ```  
  
-   **ParameterLanguage**: Provides a language for parameters passed in a URL that's independent of the browser language. The default value is the browser language. The value can be a culture value, such as **en-us** or **de-de.**
  
     For example, in native mode, to override the browser language and specify a culture value of de-DE:
  
    ```  
    https://myrshost/Reportserver?/SampleReports/Product+Line+Sales&rs:Command=Render&StartDate=4/10/2008&EndDate=11/10/2008&rs:ParameterLanguage=de-DE  
    ```  
  
-   **Snapshot**: Renders a report based on a report history snapshot. For more information, see [Render a report history snapshot using URL access](../reporting-services/render-a-report-history-snapshot-using-url-access.md).
  
     For example, in native mode, retrieve a report history snapshot dated 2003-04-07 with a time stamp of 13:40:02:
  
    ```  
    https://myrshost/reportserver?/SampleReports/Company Sales&rs:Snapshot=2003-04-07T13:40:02  
    ```  
  
-   **PersistStreams**: Renders a report in a single persisted stream. This parameter is used by the Image renderer to transmit the rendered report one chunk at a time. After using this parameter in a URL access string, use the same URL access string with the *GetNextStream* parameter instead of the *PersistStreams* parameter to get the next chunk in the persisted stream. This URL command eventually returns a 0-byte stream to indicate the end of the persisted stream. The default value is **false**.
  
-   **GetNextStream**: Gets the next data chunk in a persisted stream that's accessed by using the *PersistStreams* parameter. For more information, see the description for *PersistStreams*. The default value is **false**.
  
-   **SessionID**: Specifies an established active report session between the client application and the report server. The value of this parameter is set to the session identifier.
  
     You can specify the session ID as a cookie or as part of the URL. When the report server has been configured not to use session cookies, the first request without a specified session ID results in a redirection with a session ID. For more information about report server sessions, see [Identifying execution state](../reporting-services/report-server-web-service-net-framework-soap-headers/identifying-execution-state.md).
  
-   **ClearSession**: A value of **true** directs the report server to remove a report from the report session. All report instances associated with an authenticated user are removed from the report session. (A report instance is defined as the same report run multiple times with different report parameter values.) The default value is **false**.
  
-   **ResetSession**: A value of **true** directs the report server to reset the report session by removing the report session's association with all report snapshots. The default value is **false**.
  
-   **ShowHideToggle**: Toggles the show and hide state of a section of the report. Specify a positive integer to represent the section to toggle.
  
##  <a name="bkmk_webpart"></a> Report Viewer web part commands (rv:)
 The following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserved report parameter names are used to target the Report Viewer web part that's integrated with SharePoint. These parameter names are prefixed with *rv:*. The Report Viewer web part also accepts the *rs:ParameterLanguage* parameter.
  
-   **Toolbar**: Controls the toolbar display for the Report Viewer web part. The default value is **Full**. Values can be:
  
    -   **Full**: Displays the complete toolbar.
  
    -   **Navigation**: Displays only pagination in the toolbar.
  
    -   **None**: Doesn't display the toolbar.
  
     For example, in SharePoint mode, to display only pagination in the toolbar:
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:Toolbar=Navigation  
    ```  
  
-   **HeaderArea**: Controls the header display for the Report Viewer web part. The default value is **Full**. Values can be:
  
    -   **Full**: Displays the complete header.
  
    -   **BreadCrumbsOnly**: Displays only the breadcrumb navigation in the header to inform the user where they are in the application.
  
    -   **None**: Doesn't display the header.
  
     For example, in SharePoint mode, to display only the breadcrumb navigation in the header:
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:HeaderArea=BreadCrumbsOnly  
    ```  
  
-   **DocMapAreaWidth**: Controls the display width, in pixels, of the parameter area in the Report Viewer web part. The default value is the same as the Report Viewer web part default. The value must be a non-negative integer.
  
-   **AsyncRender**: Controls whether a report is rendered asynchronously. The default value is **true**, which specifies that a report be rendered asynchronously. The value must be a Boolean value of **true** or **false**.
  
-   **ParamMode**: Controls how the Report Viewer web part's parameter prompt area is displayed in full-page view. The default value is **Full**. Valid values are:
  
    -   **Full**: Displays the parameter prompt area.
  
    -   **Collapsed**: Collapses the parameter prompt area.
  
    -   **Hidden**: Hides the parameter prompt area.
  
     For example, in SharePoint mode, to collapse the parameter prompt area:
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:ParamMode=Collapsed  
    ```  
  
-   **DocMapMode**: Controls how the Report Viewer web part's document map area is displayed in full-page view. The default value is **Full**. Valid values are:
  
    -   **Full**: Displays the document map area.
  
    -   **Collapsed**: Collapses the document map area.
  
    -   **Hidden**: Hides the document map area.
  
-   **DockToolBar**: Controls whether the Report Viewer web part's toolbar is docked to the top or bottom. Valid values are **Top** and **Bottom**. The default value is **Top**.
  
     For example, in SharePoint mode, to dock the toolbar to the bottom:
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:DockToolBar=Bottom  
    ```  
  
-   **ToolBarItemsDisplayMode**: Controls which toolbar items are displayed. This is a bitwise enumeration value. To include a toolbar item, add the item's value to the total value. For example, for no **Actions** menu, use *rv:ToolBarItemsDisplayMode=63* (or 0x3F), which is 1+2+4+8+16+32. For **Actions** menu items only, use *rv:ToolBarItemsDisplayMode=960* (or 0x3C0). The default value is **-1**, which includes all toolbar items. Valid values are:
  
    -   **1 (0x1)**: The **Back** button  
  
    -   **2 (0x2)**: The text search controls  
  
    -   **4 (0x4)**: The page navigation controls  
  
    -   **8 (0x8)**: The **Refresh** button  
  
    -   **16 (0x10)**: The **Zoom** list box  
  
    -   **32 (0x20)**: The **Atom Feed** button  
  
    -   **64 (0x40)**: The **Print** menu option in **Actions**  
  
    -   **128 (0x80)**: The **Export** submenu in **Actions**  
  
    -   **256 (0x100)**: The **Open with Report Builder** menu option in **Actions**  
  
    -   **512 (0x200)**: The **Subscribe** menu option in **Actions**  
  
    -   **1024 (0x400)**: The **New Data Alert** menu option in **Actions**  
  
     For example, in SharePoint mode to display only the **Back** button, text search controls, page navigation controls, and the **Refresh** button:
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:ToolBarItemsDisplayMode=15  
    ```  
  
## See also
 - [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)
 - [Export a report using URL access](../reporting-services/export-a-report-using-url-access.md)
  
  
