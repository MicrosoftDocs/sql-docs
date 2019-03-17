---
title: "Report Server ExecutionLog and the ExecutionLog3 View | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "logs [Reporting Services], execution"
  - "execution logs [Reporting Services]"
ms.assetid: a7ead67d-1404-4e67-97e7-4c7b0d942070
author: markingmyname
ms.author: maghan
---
# Report Server ExecutionLog and the ExecutionLog3 View
  The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], report server execution log contains information about the reports that execute on the server or on multiple servers in a native mode scale-out deployment or a SharePoint farm. You can use the report execution log to find out how often a report is requested, what output formats are used the most, and how many milliseconds of processing time is spent on each processing phase. The log contains information on the length of time spent executing a report's dataset query and the time spent processing the data. If you are a report server administrator, you can review the log information and identify long running tasks and make suggestions to the report authors on the areas of the report (dataset or processing) they may be able to improve.  
  
 Report servers configured for SharePoint mode, can also utilize the SharePoint ULS logs. For more information, see [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](../../reporting-services/report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md)  
  
##  <a name="bkmk_top"></a> Viewing Log Information  
 The report server execution logs data about report execution into an internal database table. The information from the table is available from SQL Server views.  
  
 The report execution log is stored in the report server database that by default is named **ReportServer**. The SQL views provide the execution log information. The "2" and "3" views were added in more recent releases and contain new fields or they contain fields with friendlier names than the previous releases. The older views remain in the product so custom applications that depend on them are not impacted. If you do not have a dependence on an older view, for example ExecutionLog, it is recommended you use the most recent view, ExecutionLog**3**.  
  
 In this topic:  
  
-   [Configuration Settings for a SharePoint mode Report Server](#bkmk_sharepoint)  
  
-   [Configuration Settings for a Native Mode Report Server](#bkmk_native)  
  
-   [Log Fields (ExecutionLog3)](#bkmk_executionlog3)  
  
-   [The AdditionalInfo Field](#bkmk_additionalinfo)  
  
-   [Log Fields (ExecutionLog2)](#bkmk_executionlog2)  
  
-   [Log Fields (ExecutionLog)](#bkmk_executionlog)  
  
##  <a name="bkmk_sharepoint"></a> Configuration Settings for a SharePoint mode Report Server  
 You can turn report execution logging on or off from the system settings of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
 By default, log entries are kept 60 days. Entries that exceed this date are removed at 2:00 A.M. every day. On a mature installation, only 60 days of information will be available at any given time.  
  
 You cannot set limits on the number of rows or on the type of entries that are logged.  
  
 **To enable execution logging:**  
  
1.  From SharePoint Central Administration, click **Manage service applications** in the **Application Management** group.  
  
2.  Click the name of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application you want to configure.  
  
3.  Click **System Settings**.  
  
4.  Select **Enable Execution Logging** in the **Logging** section.  
  
5.  Click **OK**.  
  
 **To enable verbose logging:**  
  
 You need to enable logging as described in the previous steps and then complete the following:  
  
1.  From the **System Settings** page of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] services application, find the **User-defined** section.  
  
2.  Change the **ExecutionLogLevel** to **verbose**. This field is a text entry field and the two possible values are **verbose** and **normal**.  
  
##  <a name="bkmk_native"></a> Configuration Settings for a Native Mode Report Server  
 You can turn report execution logging on or off from the Server Properties page in SQL Server Management Studio. The **EnableExecutionLogging** is and Advanced property.  
  
 By default, log entries are kept 60 days. Entries that exceed this date are removed at 2:00 A.M. every day. On a mature installation, only 60 days of information will be available at any given time.  
  
 You cannot set limits on the number of rows or on the type of entries that are logged.  
  
 **To enable execution logging:**  
  
1.  Start SQL Server Management Studio with administrative privileges. For example right-click the Management Studio icon and click 'Run as administrator'.  
  
2.  Connect to the desired report server.  
  
3.  Right-click the server name and click **Properties**. If the Properties option is disabled, verify you ran SQL Server Management Studio with administrative privileges.  
  
4.  Click the **Logging** page.  
  
5.  Select **Enable report execution Logging**.  
  
 **To enable verbose logging:**  
  
 You need to enable logging as described in the previous steps and then complete the following:  
  
1.  From the **Server Properties** dialog, click the **Advanced** page.  
  
2.  In the **User-defined** section, change the **ExecutionLogLevel** to **verbose**. This field is a text entry field and the two possible values are **verbose** and **normal**.  
  
##  <a name="bkmk_executionlog3"></a> Log Fields (ExecutionLog3)  
 This view added additional performance diagnostics node inside the XML based **AdditionalInfo** column. The AdditionalInfo column contains an XML structure of 1 to many additional fields of information. The following is a sample Transact SQL statement to retrieve rows from the view ExecutionLog3. The sample assumes the report server database is named **ReportServer**:  
  
```  
Use ReportServer  
select * from ExecutionLog3 order by TimeStart DESC  
```  
  
 The following table describes the data that is captured in the report execution log  
  
|Column|Description|  
|------------|-----------------|  
|InstanceName|Name of the report server instance that handled the request. If your environment has more than one report server, you can analyze the InstanceName distribution to monitor and determine if your network-load balancer distributes requests across report servers as expected.|  
|ItemPath|Path of where a report or report item is stored.|  
|UserName|User identifier.|  
|ExecutionID|The internal identifier associated with a request. Requests on the same user sessions share the same execution id.|  
|RequestType|Possible Values:<br /><br /> Interactive<br /><br /> Subscription<br /><br /> <br /><br /> Analyzing log data filtered by RequestType=Subscription and sorted by TimeStart may reveal periods of heavy subscription usage and you may want to modify some of the report subscriptions to a different time.|  
|Format|Rendering format.|  
|Parameters|Parameter values used for a report execution.|  
|ItemAction|Possible values:<br /><br /> Render<br /><br /> Sort<br /><br /> BookMarkNavigation<br /><br /> DocumentNavigation<br /><br /> GetDocumentMap<br /><br /> Findstring<br /><br /> Execute<br /><br /> RenderEdit|  
|TimeStart|Start and stop times that indicate the duration of a report process.|  
|TimeEnd||  
|TimeDataRetrieval|Number of milliseconds spent retrieving the data.|  
|TimeProcessing|Number of milliseconds spent processing the report.|  
|TimeRendering|Number of milliseconds spent rendering the report.|  
|Source|Source of the report execution. Possible values:<br /><br /> Live<br /><br /> Cache: indicates a cached execution, for example, dataset queries are not executed live.<br /><br /> Snapshot<br /><br /> History<br /><br /> AdHoc:  Indicates either a dynamically generated report model based drill through report, or a Report Builder report that is previewed on a client utilizing the report server for processing and rendering.<br /><br /> Session: Indicates a follow up request within an already established session.  For example the initial request is to view page 1, and the follow up request is to export to Excel with the current session state.<br /><br /> Rdce:  Indicates a Report Definition Customization Extension. An RDCE custom extension can dynamically customize a report definition before it is passed to the processing engine upon report execution.|  
|Status|Status (either rsSuccess or an error code; if multiple errors occur, only the first error is recorded).|  
|ByteCount|Size of rendered reports in bytes.|  
|RowCount|Number of rows returned from queries.|  
|AdditionalInfo|An XML property bag containing additional information about the execution. The contents can be different for each row.|  
  
##  <a name="bkmk_additionalinfo"></a> The AdditionalInfo Field  
 The AdditionalInfo field is an XML property bag or structure containing additional information about the execution. The contents can be different for each row in the log.  
  
 The following are examples  of the contents of the AddtionalInfo field for both standard and verbose logging:  
  
 **Standard logging example of AddtionalInfo**  
  
```  
<AdditionalInfo>  
  <ProcessingEngine>2</ProcessingEngine>  
  <ScalabilityTime>  
    <Pagination>0</Pagination>  
    <Processing>0</Processing>  
  </ScalabilityTime>  
  <EstimatedMemoryUsageKB>  
    <Pagination>0</Pagination>  
    <Processing>6</Processing>  
  </EstimatedMemoryUsageKB>  
  <DataExtension>  
    <SQL>1</SQL>  
  </DataExtension>  
  <Connections>  
    <Connection>  
      <ConnectionOpenTime>147</ConnectionOpenTime>  
      <DataSets>  
        <DataSet>  
          <Name>DataSet1</Name>  
          <RowsRead>16</RowsRead>  
          <TotalTimeDataRetrieval>642</TotalTimeDataRetrieval>  
          <ExecuteReaderTime>63</ExecuteReaderTime>  
        </DataSet>  
        <DataSet>  
          <Name>DataSet2</Name>  
          <RowsRead>3</RowsRead>  
          <TotalTimeDataRetrieval>157</TotalTimeDataRetrieval>  
          <ExecuteReaderTime>60</ExecuteReaderTime>  
        </DataSet>  
      </DataSets>  
    </Connection>  
  </Connections>  
</AdditionalInfo>  
  
```  
  
 **Verbose logging example of AdditionalInfo**  
  
```  
<AdditionalInfo>  
  <ProcessingEngine>2</ProcessingEngine>  
  <ScalabilityTime>  
    <Pagination>0</Pagination>  
    <Processing>0</Processing>  
  </ScalabilityTime>  
  <EstimatedMemoryUsageKB>  
    <Pagination>0</Pagination>  
    <Processing>6</Processing>  
  </EstimatedMemoryUsageKB>  
  <DataExtension>  
    <SQL>1</SQL>  
  </DataExtension>  
  <Connections>  
    <Connection>  
      <ConnectionOpenTime>127</ConnectionOpenTime>  
      <DataSource>  
        <Name>DataSource1</Name>  
        <DataExtension>SQL</DataExtension>  
      </DataSource>  
      <DataSets>  
        <DataSet>  
          <Name>DataSet1</Name>  
          <RowsRead>16</RowsRead>  
          <TotalTimeDataRetrieval>655</TotalTimeDataRetrieval>  
          <QueryPrepareAndExecutionTime>94</QueryPrepareAndExecutionTime>  
          <ExecuteReaderTime>33</ExecuteReaderTime>  
          <DataReaderMappingTime>30</DataReaderMappingTime>  
          <DisposeDataReaderTime>1</DisposeDataReaderTime>  
        </DataSet>  
        <DataSet>  
          <Name>DataSet2</Name>  
          <RowsRead>3</RowsRead>  
          <TotalTimeDataRetrieval>16</TotalTimeDataRetrieval>  
          <QueryPrepareAndExecutionTime>2</QueryPrepareAndExecutionTime>  
          <ExecuteReaderTime>1</ExecuteReaderTime>  
          <DataReaderMappingTime>0</DataReaderMappingTime>  
          <DisposeDataReaderTime>0</DisposeDataReaderTime>  
        </DataSet>  
      </DataSets>  
    </Connection>  
  </Connections>  
</AdditionalInfo>  
  
```  
  
 The following are some of the values you will see in the AdditionalInfo field:  
  
-   **ProcessingEngine**  
  
     1=SQL Server 2005, 2=The new On-demand Processing Engine. If a majority of your reports are still showing the value of 1, you may investigate how to redesign them so they utilize the newer and more efficient on-demand processing engine.  
  
     `<ProcessingEngine>2</ProcessingEngine>`  
  
-   **ScalabilityTime**  
  
     The number of milliseconds spent performing scale related operations in the processing engine. A value of 0 indicates that no additional time was spent on scale operations and a 0 also indicates the request was not under memory pressure.  
  
    ```  
    <ScalabilityTime>  
        <Processing>0</Processing>  
    </ScalabilityTime>  
    ```  
  
-   **EstimatedMemoryUsageKB**  
  
     An estimate of the peak amount of memory, in kilobytes, consumed by each component during a particular request.  
  
    ```  
    <EstimatedMemoryUsageKB>  
        <Processing>38</Processing>  
    </EstimatedMemoryUsageKB>  
    ```  
  
-   **DataExtension**  
  
     The types of data extensions or data sources used in the report. The number is a count of the number of occurrences of the particular data source.  
  
    ```  
    <DataExtension>  
       <DAX>2</DAX>  
    </DataExtension>  
    ```  
  
-   **ExternalImages**  
  
     Added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
     The value is in miliseconds. This data can be used to diagnose performance issues. The time needed to retrieve images from an external webserver may slow the overall report execution.  
  
    ```  
    <ExternalImages>  
        <Count>3</Count>  
        <ByteCount>9268</ByteCount>  
        <ResourceFetchTime>9</ResourceFetchTime>  
    </ExternalImages>  
    ```  
  
-   **Connections**  
  
     Added in [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
     A multi-leveled structure  
  
    ```  
    <Connections>  
        <Connection>  
          <ConnectionOpenTime>127</ConnectionOpenTime>  
          <DataSource>  
            <Name>DataSource1</Name>  
            <DataExtension>SQL</DataExtension>  
          </DataSource>  
          <DataSets>  
            <DataSet>  
              <Name>DataSet1</Name>  
              <RowsRead>16</RowsRead>  
              <TotalTimeDataRetrieval>655</TotalTimeDataRetrieval>  
              <QueryPrepareAndExecutionTime>94</QueryPrepareAndExecutionTime>  
              <ExecuteReaderTime>33</ExecuteReaderTime>  
              <DataReaderMappingTime>30</DataReaderMappingTime>  
              <DisposeDataReaderTime>1</DisposeDataReaderTime>  
            </DataSet>  
            <DataSet>  
              <Name>DataSet2</Name>  
              <RowsRead>3</RowsRead>  
              <TotalTimeDataRetrieval>16</TotalTimeDataRetrieval>  
              <QueryPrepareAndExecutionTime>2</QueryPrepareAndExecutionTime>  
              <ExecuteReaderTime>1</ExecuteReaderTime>  
              <DataReaderMappingTime>0</DataReaderMappingTime>  
              <DisposeDataReaderTime>0</DisposeDataReaderTime>  
            </DataSet>  
          </DataSets>  
        </Connection>  
    </Connections>  
  
    ```  
  
##  <a name="bkmk_executionlog2"></a> Log Fields (ExecutionLog2)  
 This view added a few new fields and renamed a few others. The following is a sample Transact SQL statement to retrieve rows from the view ExecutionLog2. The sample assumes the report server database is named **ReportServer**:  
  
```  
Use ReportServer  
select * from ExecutionLog2 order by TimeStart DESC  
```  
  
 The following table describes the data that is captured in the report execution log  
  
|Column|Description|  
|------------|-----------------|  
|InstanceName|Name of the report server instance that handled the request.|  
|ReportPath|The path structure to the report.  For example a report named "test" which is the in root folder in Report Manager, would have a ReportPath of "/test".<br /><br /> A report named "test" that is saved in the folder "samples" on Report Manager , will have a ReportPath of "/Samples/test/"|  
|UserName|User identifier.|  
|ExecutionID||  
|RequestType|Request type (either user or system).|  
|Format|Rendering format.|  
|Parameters|Parameter values used for a report execution.|  
|ReportAction|Possible values: Render, Sort, BookMarkNavigation, DocumentNavigation, GetDocumentMap, Findstring|  
|TimeStart|Start and stop times that indicate the duration of a report process.|  
|TimeEnd||  
|TimeDataRetrieval|Number of milliseconds spent retrieving the data, processing the report, and rendering the report.|  
|TimeProcessing||  
|TimeRendering||  
|Source|Source of the report execution (1=Live, 2=Cache, 3=Snapshot, 4=History).|  
|Status|Status (either rsSuccess or an error code; if multiple errors occur, only the first error is recorded).|  
|ByteCount|Size of rendered reports in bytes.|  
|RowCount|Number of rows returned from queries.|  
|AdditionalInfo|An XML property bag containing additional information about the execution.|  
  
##  <a name="bkmk_executionlog"></a> Log Fields (ExecutionLog)  
 The following is a sample Transact SQL statement to retrieve rows from the view ExecutionLog. The sample assumes the report server database is named **ReportServer**:  
  
```  
Use ReportServer  
select * from ExecutionLog order by TimeStart DESC  
  
```  
  
 The following table describes the data that is captured in the report execution log  
  
|Column|Description|  
|------------|-----------------|  
|InstanceName|Name of the report server instance that handled the request.|  
|ReportID|Report identifier.|  
|UserName|User identifier.|  
|RequestType|Possible values:<br /><br /> True = A Subscription request<br /><br /> False= An Interactive request|  
|Format|Rendering format.|  
|Parameters|Parameter values used for a report execution.|  
|TimeStart|Start and stop times that indicate the duration of a report process.|  
|TimeEnd||  
|TimeDataRetrieval|Number of milliseconds spent retrieving the data, processing the report, and rendering the report.|  
|TimeProcessing||  
|TimeRendering||  
|Source|Source of the report execution. Possible values: (1=Live, 2=Cache, 3=Snapshot, 4=History, 5=Adhoc, 6=Session, 7=RDCE).|  
|Status|Possible values: rsSuccess, rsProcessingAborted, or an error code. If multiple errors occur, only the first error is recorded.|  
|ByteCount|Size of rendered reports in bytes.|  
|RowCount|Number of rows returned from queries.|  
  
## See Also  
 [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](../../reporting-services/report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md)   
 [Reporting Services Log Files and Sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md)   
 [Errors and Events Reference &#40;Reporting Services&#41;](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)  
  
  
