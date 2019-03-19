---
title: "URL Access Parameter Reference | Microsoft Docs"
ms.date: 09/09/2015
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], display options"
  - "URL access [Reporting Services], report display parameters"
ms.assetid: 1c3e680a-83ea-4979-8e79-fa2337ae12a3
author: markingmyname
ms.author: maghan
---
# URL Access Parameter Reference
  You can use the following parameters as part of a URL to configure the look and feel of your [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]reports. The most common parameters are listed in this section. Parameters are case-insensitive and begin with the parameter prefix *rs:* if directed to the report server and *rc:* if directed to an HTML Viewer. You can also specify parameters that are specific to devices or rendering extensions. For more information about device-specific parameters, see [Specify Device Information Settings in a URL](../reporting-services/specify-device-information-settings-in-a-url.md).  
  
> [!IMPORTANT]  
>  For a SharePoint mode report server it  is important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds context to the HTTP reques that is required to ensure proper execution of the report for SharePoint mode report servers. For examples, see [Access Report Server Items Using URL Access](../reporting-services/access-report-server-items-using-url-access.md).  
>   
>  For information about including report parameters in a URL, and examples, see [Pass a Report Parameter Within a URL](../reporting-services/pass-a-report-parameter-within-a-url.md).  
  
##  <a name="bkmk_top"></a> In this topic  
  
-   [HTML Viewer Commands (rc:)](#bkmk_htmlviewer)  
  
-   [Report Server Commands (rs:)](#bkmk_reportserver)  
  
-   [Report Viewer Web Part Commands (rv:)](#bkmk_webpart)  
  
##  <a name="bkmk_htmlviewer"></a> HTML Viewer Commands (rc:)  
 HTML Viewer commands are used to target the HTML Viewer (for example, from Report Manager) and are prefixed with *rc:*:  
  
-   *Toolbar* :  
                  Shows or hides the toolbar. If the value of this parameter is **false**, all remaining options are ignored. If you omit this parameter, the toolbar is automatically displayed for rendering formats that support it. The default of this parameter is **true**.  
  
    > [!IMPORTANT]  
    >  *rc:Toolbar*=**false** does not work for URL access strings that use an IP address, instead of a domain name, to target a report hosted on a SharePoint site.  
  
-   *Parameters* : Shows or hides the parameters area of the toolbar. If you set this parameter to **true**, the parameters area of the toolbar is displayed. If you set this parameter to **false**, the parameters area is not displayed and cannot be displayed by the user. If you set this parameter to a value of **Collapsed**, the parameters area will not be displayed, but can be toggled by the end user. The default value of this parameter is **true**.  
  
     For an example in **Native** mode:  
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Parameters=Collapsed  
    ```  
  
     For an example in **SharePoint** mode:  
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Parameters=Collapsed  
    ```  
  
-   *Zoom* : Sets the report zoom value as an integer percentage or a string constant. Standard string values include **Page Width** and **Whole Page**. This parameter is ignored by versions of Internet Explorer earlier than Internet Explorer 5.0 and all non-[!INCLUDE[msCoName](../includes/msconame-md.md)] browsers. The default value of this parameter is **100**.  
  
     For example in **Native** mode:  
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Zoom=Page Width  
    ```  
  
     For example in **SharePoint** mode.  
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Zoom=Page Width  
    ```  
  
-   *Section* : Sets which page in the report to display. Any value that is greater than the number of pages in the report displays the last page. Any value that is less than **0** displays page 1 of the report. The default value of this parameter is **1**.  
  
     For example in **Native** mode, to display page 2 of the report:  
  
    ```  
    https://myrshost/reportserver?/Sales&rc:Section=2  
    ```  
  
     For example in **SharePoint** mode, to display page 2 of the report:  
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:Section=2  
    ```  
  
-   *FindString*: Search a report for a specific set of text.  
  
     For an example in **Native** mode.  
  
    ```  
    https://myrshost/reportserver?/Sales&rc:FindString=Mountain-400  
    ```  
  
     For an example in **SharePoint** mode.  
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rc:FindString=Mountain-400  
    ```  
  
-   *StartFind* : Specifies the last section to search. The default value of this parameter is the last page of the report.  
  
     For an example in **Native** mode that searches for the first occurrence of the text "Mountain-400" in the Product Catalog sample report starting with page one and ending with page five.  
  
    ```  
    https://server/Reportserver?/SampleReports/Product Catalog&rs:Command=Render&rc:StartFind=1&rc:EndFind=5&rc:FindString=Mountain-400  
    ```  
  
-   *EndFind* : Sets the number of the last page to use in the search. For example, a value of **5** indicates that the last page to be searched is page 5 of the report. The default value is the number of the current page. Use this parameter in conjunction with the *StartFind* parameter. See the above example.  
  
-   *FallbackPage* : Sets the number of the page to display if a search or a document map selection fails. The default value is the number of the current page.  
  
-   *GetImage* : Gets a particular icon for the HTML Viewer user interface.  
  
-   *Icon* : Gets the icon of a particular rendering extension.  
  
-   *Stylesheet*: Specifies a style sheet to be applied to the HTML Viewer.  
  
-   Device Information Setting: Specifies a device information setting in the form of `rc:tag=value`, where *tag* is the name of a device information setting specific to the rendering extension that is currently used (see the description for the *Format* parameter). For example, you can use the *OutputFormat* device information setting for the IMAGE rendering extension to render the report to a JPEG image using the following parameters in the URL access string: `...&rs:Format=IMAGE&rc:OutputFormat=JPEG`. For more information on all extension-specific device information settings, see [Device Information Settings for Rendering Extensions &#40;Reporting Services&#41;](../reporting-services/device-information-settings-for-rendering-extensions-reporting-services.md).  
  
##  <a name="bkmk_reportserver"></a> Report Server Commands (rs:)  
 Report server commands are prefixed with *rs:* and are used to target the report server:  
  
-   *Command*:  
                  Performs an action on a catalog item, depending on its item type. The default value is determined by the type of the catalog item referenced in the URL access string. Valid values are:  
  
    -   **ListChildren** and **GetChildren** Displays the contents of a folder. The folder items are displayed within a generic item-navigation page.  
  
         For example in **Native** mode.  
  
        ```  
        https://myrshost/reportserver?/Sales&rs:Command=GetChildren  
        ```  
  
         For example, a named instance in **Native** mode.  
  
        ```  
        https://myssrshost/Reportserver_THESQLINSTANCE?/reportfolder&rs:Command=listChildren  
        ```  
  
         For example in **SharePoint** mode.  
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales&rs:Command=GetChildren  
        ```  
  
    -   **Render** The report is rendered in the browser so you can view it.  
  
         For example in **Native** mode:  
  
        ```  
        https://myrshost/reportserver?/Sales/YearlySalesByCategory&rs:Command=Render  
        ```  
  
         For example in **SharePoint** mode.  
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/YearlySalesByCategory&rs:Command=Render  
        ```  
  
    -   **GetSharedDatasetDefinition** Displays the XML definition associated with a shared dataset. Shared dataset properties, including the query, dataset parameters, default values, dataset filters, and data options such as collation and case sensitivity, are saved in the definition. You must have **Read Report Definition** permission on a shared dataset to use this value.  
  
         For example in **Native** mode.  
  
        ```  
        https://localhost/reportserver/?/DataSet1&rs:command=GetShareddatasetDefinition  
        ```  
  
    -   **GetDataSourceContents** Displays the properties of a given shared data source as XML. If your browser supports XML and if you are an authenticated user with **Read Contents** permission on the data source, the data source definition is displayed.  
  
         For example in **Native** mode.  
  
        ```  
        https://myrshost/reportserver?/Sales/AdventureWorks2012&rs:Command=GetDataSourceContents  
        ```  
  
         For example in **SharePoint** mode.  
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/AdventureWorks2012&rs:Command=GetDataSourceContents  
        ```  
  
    -   **GetResourceContents** Renders a resource and displays it in an HTML page if the resource is compatible with the browser. Otherwise, you are prompted to open or save the file or resource to disk.  
  
         For example in **Native** mode.  
  
        ```  
        https://myrshost/reportserver?/Sales/StorePicture&rs:Command=GetResourceContents  
        ```  
  
         For example in **SharePoint** mode.  
  
        ```  
        https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/Sales/StorePicture.jpg&rs:Command=GetResourceContents  
        ```  
  
    -   **GetComponentDefinition** Displays the XML definition associated with a published report item. You must have **Read Contents** permission on a published report item to use this value.  
  
-   *Format* :  
                  Specifies the format in which to render and view a report. Common values include:  
  
    -   **HTML5**  
  
    -   **PPTX**  
  
    -   **ATOM**  
  
    -   **HTML4.0**  
  
    -   **MHTML**  
  
    -   **IMAGE**  
  
    -   **EXCEL**  
  
    -   **WORD**  
  
    -   **CSV**  
  
    -   **PDF**  
  
    -   **XML**  
  
     The default value is **HTML5**. For more information, see [Export a Report Using URL Access](../reporting-services/export-a-report-using-url-access.md).  
  
     For a complete list, see the **\<Render>** extension section of the report server rsreportserver.config file.  For information on where to find the file, see [RsReportServer.config Configuration File](../reporting-services/report-server/rsreportserver-config-configuration-file.md).  
  
     For example, to get a PDF copy of a report directly from a **Native** mode report server:  
  
    ```  
    https://myrshost/ReportServer?/myreport&rs:Format=PDF  
    ```  
  
     For example, to get a PDF copy of a report directly from a **SharePoint** mode report server:  
  
    ```  
    https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
    ```  
  
-   *ParameterLanguage*:  
                  Provides a language for parameters passed in a URL that is independent of the browser language. The default value is the browser language. The value can be a culture value, such as **en-us** or **de-de.**  
  
     For example in **Native** mode, to override the browser language and specify a culture value of de-DE:  
  
    ```  
    https://myrshost/Reportserver?/SampleReports/Product+Line+Sales&rs:Command=Render&StartDate=4/10/2008&EndDate=11/10/2008&rs:ParameterLanguage=de-DE  
    ```  
  
-   *Snapshot* : Renders a report based on a report history snapshot. For more information, see [Render a Report History Snapshot Using URL Access](../reporting-services/render-a-report-history-snapshot-using-url-access.md).  
  
     For example in **Native** mode, retrieve a report history snapshot dated 2003-04-07 with a timestamp of 13:40:02:  
  
    ```  
    https://myrshost/reportserver?/SampleReports/Company Sales&rs:Snapshot=2003-04-07T13:40:02  
    ```  
  
-   *PersistStreams*:  
                  Renders a report in a single persisted stream. This parameter is used by the Image renderer to transmit the rendered report one chunk at a time. After using this parameter in a URL access string, use the same URL access string with the *GetNextStream* parameter instead of the *PersistStreams* parameter to get the next chunk in the persisted stream. This URL command will eventually return a 0-byte stream to indicate the end of the persisted stream. The default value is **false**.  
  
-   *GetNextStream*:  
                  Gets the next data chunk in a persisted stream that is accessed using the *PersistStreams* parameter. For more information, see the description for *PersistStreams*. The default value is **false**.  
  
-   *SessionID*:  
                  Specifies an established active report session between the client application and the report server. The value of this parameter is set to the session identifier.  
  
     You can specify the session ID as a cookie or as part of the URL. When the report server has been configured not to use session cookies, the first request without a specified session ID results in a redirection with a session ID. For more information about report server sessions, see [Identifying Execution State](../reporting-services/report-server-web-service-net-framework-soap-headers/identifying-execution-state.md).  
  
-   *ClearSession*:  
                  A value of **true** directs the report server to remove a report from the report session. All report instances associated with an authenticated user are removed from the report session. (A report instance is defined as the same report run multiple times with different report parameter values.) The default value is **false**.  
  
-   *ResetSession*:  
                  A value of **true** directs the report server to reset the report session by removing the report session's association with all report snapshots. The default value is **false**.  
  
-   *ShowHideToggle*:  
                  Toggles the show and hide state of a section of the report. Specify a positive integer to represent the section to toggle.  
  
##  <a name="bkmk_webpart"></a> Report Viewer Web Part Commands (rv:)  
 The following [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] reserved report parameter names are used to target the Report Viewer Web Part that is integrated with SharePoint. These parameter names are prefixed with *rv:*. The Report Viewer Web Part also accepts the *rs:ParameterLanguage* parameter.  
  
-   *Toolbar*: Controls the toolbar display for the Report Viewer Web Part. The default value is **Full**. Values can be:  
  
    -   **Full**: display the complete toolbar.  
  
    -   **Navigation**: display only pagination in the toolbar.  
  
    -   **None**: do not display the toolbar.  
  
     For example in **SharePoint** mode, to display only pagination in the toolbar.  
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:Toolbar=Navigation  
    ```  
  
-   *HeaderArea*: Controls the header display for the Report Viewer Web Part. The default value is **Full**. Values can be:  
  
    -   **Full**: display the complete header.  
  
    -   **BreadCrumbsOnly**: display only the bread-crumb navigation in the header to inform the user where they are in the application.  
  
    -   **None**: do not display the header.  
  
     For example in **SharePoint** mode, to display only the bread-crumb navigation in the header.  
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:HeaderArea=BreadCrumbsOnly  
    ```  
  
-   *DocMapAreaWidth*: Controls the display width, in pixels, of the parameter area in the Report Viewer Web Part. The default value is the same as the Report Viewer Web Part default. The value must be a non-negative integer.  
  
-   *AsyncRender*: Controls whether a report is rendered asynchronously. The default value is **true**, which specifies that a report be rendered asynchronously. The value must be a Boolean value of **true** or **false**.  
  
-   *ParamMode*: Controls how the Report Viewer Web Part's parameter prompt area is displayed in full-page view. The default value is **Full**. Valid values are:  
  
    -   **Full**: display the parameter prompt area.  
  
    -   **Collapsed**: collapse the parameter prompt area.  
  
    -   **Hidden**: hide the parameter prompt area.  
  
     For example in **SharePoint** mode, to collapse the parameter prompt area.  
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:ParamMode=Collapsed  
    ```  
  
-   *DocMapMode*: Controls how the Report Viewer Web Part's document map area is displayed in full-page view. The default value is **Full**. Valid values are:  
  
    -   **Full**: display the document map area.  
  
    -   **Collapsed**: collapse the document map area.  
  
    -   **Hidden**: hide the document map area.  
  
-   *DockToolBar*: Controls whether the Report Viewer Web Part's toolbar is docked to the top or bottom. Valid values are **Top** and **Bottom**. The default value is **Top**.  
  
     For example in **SharePoint** mode, to dock the toolbar to the bottom.  
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:DockToolBar=Bottom  
    ```  
  
-   *ToolBarItemsDisplayMode*: Controls which toolbar items are displayed. This is a bitwise enumeration value. To include a toolbar item, add the item's value to the total value. For example: for no Actions menu, use rv:ToolBarItemsDisplayMode=63 (or 0x3F), which is 1+2+4+8+16+32; for Actions menu items only, use rv:ToolBarItemsDisplayMode=960 (or 0x3C0). The default value is **-1**, which includes all toolbar items. Valid values are:  
  
    -   1 (0x1): the **Back** button  
  
    -   2 (0x2): the text search controls  
  
    -   4 (0x4): the page navigation controls  
  
    -   8 (0x8): the **Refresh** button  
  
    -   16 (0x10): the **Zoom** list box  
  
    -   32 (0x20): the **Atom Feed** button  
  
    -   64 (0x40): the **Print** menu option in **Actions**  
  
    -   128 (0x80): the **Export** submenu in **Actions**  
  
    -   256 (0x100: the **Open with Report Builder** menu option in **Actions**  
  
    -   512 (0x200: the **Subscribe** menu option in **Actions**  
  
    -   1024 (0x400: the **New Data Alert** menu option in **Actions**  
  
     For example, in **SharePoint** mode to display only the **Back** button, text search controls, page navigation controls, and the **Refresh** button.  
  
    ```  
    https://myspsite/_vti_bin/reportserver?https://myspsite002%fShared+Documents%2fmyreport.rdl&rv:DocMapMode=Displayed&rv:ToolBarItemsDisplayMode=15  
    ```  
  
## See Also  
 [URL Access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
 [Export a Report Using URL Access](../reporting-services/export-a-report-using-url-access.md)  
  
  
