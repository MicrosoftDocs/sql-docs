---
title: Use ExecutionLog and the ExecutionLog3 view in Reporting Services
description: See the report server execution log that's in Reporting Services, which contains information about reports on servers in native mode or a SharePoint farm.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "logs [Reporting Services], execution"
  - "execution logs [Reporting Services]"
# customer intent: As a Reporting Services user, I want to see how to use ExecutionLog and the ExecutionLog3 view so that I can access information about my report server.
---
# Use ExecutionLog and the ExecutionLog3 view in Reporting Services

The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server execution log contains information about the reports that execute on the server or on multiple servers. These servers are in a native mode scale-out deployment or a SharePoint farm. You can use the report execution log to find out:

- The number of times a report is requested.
- What output formats are most used.
- The processing time spent on each processing phase in milliseconds.
  
The log contains information on the length of time spent running a report's dataset query and the time spent processing the data. If you're a report server administrator, you can review the log information and identify long running tasks. You can also make suggestions to the report authors on the areas of the dataset or processing report they might be able to improve.  
  
Report servers configured for SharePoint mode can also utilize the SharePoint Unified Logging Service (ULS) logs. For more information, see [Turn on Reporting Services events for the SharePoint trace log &#40;ULS&#41;](../../reporting-services/report-server/turn-on-reporting-services-events-for-the-sharepoint-trace-log-uls.md)  
  
## <a name="bkmk_top"></a> View log information  

The report server logs data about report execution into an internal database table. The information from the table is available from SQL Server views.  
  
The report execution log is stored in the report server database that by default is named ReportServer. The SQL views provide the execution log information. The "2" and "3" views were added in more recent releases and contain new fields, or they contain fields with friendlier names than the previous releases. The older views remain in the product so custom applications that depend on them aren't impacted. If you don't have a dependence on an older view, for example ExecutionLog, you should use the most recent view, **ExecutionLog3**.
  
## <a name="bkmk_sharepoint"></a> Configuration settings for a SharePoint mode report server  

You can turn report execution logging on or off from the system settings of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.  
  
By default, log entries are kept 60 days. Entries that exceed this date are removed at 2:00 A.M. every day. On a mature installation, only 60 days of information is available at any given time.  
  
You can't set limits on the number of rows or on the type of entries that are logged.  
  
### Enable execution logging for a SharePoint server
  
1. From SharePoint Central Administration, select **Manage service applications** in the **Application Management** group.  
  
1. Choose the name of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application you want to configure.  
  
1. Select **System Settings**.  
  
1. Select **Enable Execution Logging** in the **Logging** section.  

1. Select **OK**.  
  
### Enable verbose logging for a SharePoint server
  
You need to enable logging as described in the previous steps and then complete the following steps:  
  
1. From the **System Settings** page of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] services application, find the **User-defined** section.  
  
1. Change the **ExecutionLogLevel** to **verbose**. This field is a text entry field and the two possible values are **verbose** and **normal**.  
  
## <a name="bkmk_native"></a> Configuration settings for a native mode report server  

You can turn report execution logging on or off from the Server Properties page in SQL Server Management Studio. The **EnableExecutionLogging** is an advanced property.  
  
By default, log entries are kept 60 days. Entries that exceed this date are removed at 2:00 A.M. every day. On a mature installation, only 60 days of information are available at any given time.  
  
You can't set limits on the number of rows or on the type of entries that are logged.  
  
### Enable execution logging for a native mode server
  
1. Start SQL Server Management Studio with administrative privileges. For example, right-click the Management Studio icon and select **Run as administrator**.  
  
1. Connect to the desired report server.  
  
1. Right-click the server name and select **Properties**. If the **Properties** option is disabled, verify you ran SQL Server Management Studio with administrative privileges.  
  
1. Select the **Logging** page.  
  
1. Select **Enable report execution Logging**.  
  
## Enable verbose logging for a native mode server
  
You need to enable logging as described in the previous steps and then complete the following steps:  
  
1. From the **Server Properties** dialog, select the **Advanced** page.  
  
1. In the **User-defined** section, change the **ExecutionLogLevel** to **verbose**. This field is a text entry field and the two possible values are **verbose** and **normal**.

    :::image type="content" source="media/report-server-executionlog-and-the-executionlog3-view/server-properties-verbose.png" alt-text="Screenshot of the Server Properties dialog box highlighting the ExecutionLogLevel field set to verbose.":::
  
## <a name="bkmk_executionlog3"></a> Log fields (ExecutionLog3)  

This view adds more performance diagnostics nodes inside the XML based **AdditionalInfo** column. The **AdditionalInfo** column contains an XML structure of 1 to many fields of information. The following example shows a Transact SQL statement that retrieves rows from the view ExecutionLog3. The sample assumes the report server database is named ReportServer:  
  
``` tsql
Use ReportServer  
select * from ExecutionLog3 order by TimeStart DESC  
```  
  
The following table describes the data that's captured in the report execution log.  
  
|Column|Description|  
|------------|-----------------|  
|InstanceName|Name of the report server instance that handled the request. If your environment has more than one report server, you can analyze the InstanceName distribution to monitor and determine if your network-load balancer distributes requests across report servers as expected.|  
|ItemPath|Path of where a report or report item is stored.|  
|UserName|User identifier.|  
|ExecutionID|The internal identifier associated with a request. Requests on the same user sessions share the same execution ID.|  
|RequestType|Possible Values:<br /><br /> Interactive<br /><br /> Subscription<br /><br /> <br /><br /> Analyzing log data filtered by `RequestType=Subscription` and sorted by `TimeStart` might reveal periods of heavy subscription usage and you might want to modify some of the report subscriptions to a different time.|  
|Format|Rendering format.|  
|Parameters|Parameter values used for a report execution.|  
|ItemAction|Possible values:<br /><br /> `Render`<br /><br /> `Sort`<br /><br /> `BookMarkNavigation`<br /><br /> `DocumentNavigation`<br /><br /> `GetDocumentMap`<br /><br /> `Findstring`<br /><br /> `Execute`<br /><br /> `RenderEdit`.|  
|TimeStart|Start and stop times that indicate the duration of a report process.|  
|TimeEnd||  
|TimeDataRetrieval|Number of milliseconds spent retrieving the data.|  
|TimeProcessing|Number of milliseconds spent processing the report.|  
|TimeRendering|Number of milliseconds spent rendering the report.|  
|Source|Source of the report execution. Possible values:<br /><br /> Live<br /><br /> Cache: indicates a cached execution, for example, dataset queries aren't executed live.<br /><br /> Snapshot<br /><br /> History<br /><br /> AdHoc: Indicates either a dynamically generated report model based drill through report. It can also refer to a Report Builder report that's previewed on a client utilizing the report server for processing and rendering.<br /><br /> Session: Indicates a follow-up request within an already established session. For example, the initial request is to view page 1, and the follow-up request is to export to Excel with the current session state.<br /><br /> Report Definition Customization Extension (RDCE):  Indicates a Report Definition Customization Extension. An RDCE custom extension can dynamically customize a report definition before the definition is passed to the processing engine upon report execution.|  
|Status|Status (either rsSuccess or an error code; if multiple errors occur, only the first error is recorded).|  
|ByteCount|Size of rendered reports in bytes.|  
|RowCount|Number of rows returned from queries.|  
|AdditionalInfo|An XML property bag containing additional information about the execution. The contents can be different for each row.|  
  
## <a name="bkmk_additionalinfo"></a> The AdditionalInfo field  

The **AdditionalInfo** field is an XML property bag or structure containing additional information about the execution. The contents can be different for each row in the log.  
  
The following are examples of the contents of the **AdditionalInfo** field for both standard and verbose logging:  
  
### Standard logging example of AdditionalInfo  
  
``` xml
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
  
### Verbose logging example of AdditionalInfo  
  
```  xml
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
  
 The following examples are some of the values you see in the **AdditionalInfo** field:  
  
- **ProcessingEngine**

    If most of your reports are still showing the value of 1, you might investigate how to redesign them so that they utilize the newer and more efficient on-demand processing engine.

    ``` xml
    1=SQL Server 2005, 2=The new On-demand Processing Engine
    ```

    ``` xml
    <ProcessingEngine>2</ProcessingEngine>
    ```
  
- **ScalabilityTime**  
  
    The number of milliseconds spent performing scale related operations in the processing engine. A value of `0` indicates that no other time was spent on scale operations and a `0` also indicates the request isn't under memory pressure.  
  
    ```  xml
    <ScalabilityTime>  
        <Processing>0</Processing>  
    </ScalabilityTime>  
    ```  
  
- **EstimatedMemoryUsageKB**  
  
    An estimate of the peak amount of memory, in kilobytes, consumed by each component during a particular request.  
  
    ```  xml
    <EstimatedMemoryUsageKB>  
        <Processing>38</Processing>  
    </EstimatedMemoryUsageKB>  
    ```  
  
- **DataExtension**  
  
    The types of data extensions or data sources used in the report. The number is a count of the number of occurrences of the particular data source.  
  
    ```  xml
    <DataExtension>  
       <DAX>2</DAX>  
    </DataExtension>  
    ```  
  
- **ExternalImages**  
  
    The value is in milliseconds. This data can be used to diagnose performance issues. The time needed to retrieve images from an external web server might slow the overall report execution.  
  
    ```  xml
    <ExternalImages>  
        <Count>3</Count>  
        <ByteCount>9268</ByteCount>  
        <ResourceFetchTime>9</ResourceFetchTime>  
    </ExternalImages>  
    ```  
  
- **Connections**  
  
    A multi-leveled structure  
  
    ```  xml
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
  
## <a name="bkmk_executionlog2"></a> Log fields (ExecutionLog2)  

This view added a few new fields and renamed a few others. The following sample is a Transact SQL statement that retrieves rows from the view **ExecutionLog2**. The sample assumes the report server database is named `ReportServer`:  
  
``` tsql
Use ReportServer  
select * from ExecutionLog2 order by TimeStart DESC  
```  
  
The following table describes the data that's captured in the report execution log.  
  
|Column|Description|  
|------------|------------------------------------------------------------|  
|InstanceName|Name of the report server instance that handled the request.|  
|ReportPath|The path structure to the report. A report saved in the root folder as **test**, has a ReportPath of `/test`.<br /><br /> A report named **test** that's saved in the folder **Samples**, would have a ReportPath of `/Samples/test/`.|  
|UserName|User identifier.|  
|ExecutionID||  
|RequestType|Request type is either `user` or `system`.|  
|Format|Rendering format.|  
|Parameters|Parameter values used for a report execution.|  
|ReportAction|Possible values: `Render`, `Sort`, `BookMarkNavigation`, `DocumentNavigation`, `GetDocumentMap`, `Findstring`.|  
|TimeStart|Start time that indicates the duration of a report process.|
|TimeEnd|End time that indicates the duration of a report process.|
|TimeDataRetrieval|Number of milliseconds spent retrieving the data.|
|TimeProcessing|Number of milliseconds spent processing the report.|
|TimeRendering|Number of milliseconds spent rendering the report.|
|Source|Source of the report execution. Options are `1=Live`, `2=Cache`, `3=Snapshot`, `4=History`.|  
|Status|Status is either `rsSuccess` or an error code. If multiple errors occur, only the first error is recorded.|  
|ByteCount|Size of rendered reports in bytes.|  
|RowCount|Number of rows returned from queries.|  
|AdditionalInfo|An XML property bag containing additional information about the execution.|  
  
## <a name="bkmk_executionlog"></a> Log fields (ExecutionLog)  

The following sample is a Transact SQL statement that retrieves rows from the view ExecutionLog. The sample assumes the report server database is named `ReportServer`:  
  
``` tsql
Use ReportServer  
select * from ExecutionLog order by TimeStart DESC
```  
  
 The following table describes the data that's captured in the report execution log.  
  
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
|Status|Possible values are `rsSuccess`, `rsProcessingAborted`, or an error code. If multiple errors occur, only the first error is recorded.|  
|ByteCount|Size of rendered reports in bytes.|  
|RowCount|Number of rows returned from queries.|  
  
## Related content

- [Reporting Services log files and sources](../../reporting-services/report-server/reporting-services-log-files-and-sources.md)
- [Errors and events reference &#40;Reporting Services&#41;](../../reporting-services/troubleshooting/errors-and-events-reference-reporting-services.md)
