---
title: "Analysis Services MDX Query Designer User Interface | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10012"
  - "sql12.rtp.rptdesigner.dataview.asquerydesigner.f1"
helpviewer_keywords: 
  - "MDX [Reporting Services], creating datasets"
  - "query designers [Reporting Services]"
ms.assetid: d9c7c0b3-fce4-4a65-b679-408273e6a925
author: markingmyname
ms.author: maghan
manager: craigg
---
# Analysis Services MDX Query Designer User Interface
  [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] provides graphical query designers for building Multidimensional Expression (MDX) queries and Data Mining Expression (DMX) queries for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source. This topic describes the MDX query designer. For more information about the DMX query designer, see [Analysis Services Connection Type for DMX &#40;SSRS&#41;](analysis-services-connection-type-for-dmx-ssrs.md).  
  
 The MDX graphical query designer has two modes: design mode and query mode. Each mode provides a metadata pane from which you can drag members from the selected cubes to build an MDX query that retrieves data when the report is processed.  
  
> [!IMPORTANT]  
>  Users access data sources when they create and run queries. You should grant minimal permissions on the data sources, such as read-only permissions.  
  
## Graphical MDX Query Designer in Design Mode  
 When you edit an MDX query for a report dataset, the graphical MDX query designer opens in Design mode.  
  
 The following figure labels the panes for Design mode.  
  
 ![Analysis Services MDX query designer, design view](../../analysis-services/media/rsqd-dsawas-mdx-designmode.gif "Analysis Services MDX query designer, design view")  
  
 The following table lists the panes in this mode:  
  
|Pane|Function|  
|----------|--------------|  
|Select Cube button (**...**)|Displays the currently selected cube.|  
|Metadata pane|Displays a hierarchical list of measures, Key Performance Indicators (KPIs), and dimensions defined on the selected cube.|  
|Calculated Members pane|Displays the currently defined calculated members available for use in the query.|  
|Filter pane|Use to choose dimensions and related hierarchies to filter data at the source and limit data returned to the report.|  
|Data pane|Displays the column headings for the result set as you drag items from the Metadata pane and the Calculated Members pane. Automatically updates the result set if the **AutoExecute** button is selected. .|  
  
 You can drag dimensions, measures, and KPIs from the Metadata pane, and calculated members from the Calculated Member pane, onto the Data pane. In the Filter pane, you can select dimensions and related hierarchies, and set filter expressions to limit the data available to query. If the **AutoExecute** (![AutoExecute the query](../../analysis-services/media/rsqdicon-autoexecute.gif "AutoExecute the query")) toggle button on the toolbar is selected, the query designer runs the query every time that you drop a metadata object onto the Data pane. You can manually run the query using the **Run** (![Run the query](../../analysis-services/media/rsqdicon-run.gif "Run the query")) button on the toolbar.  
  
 When you create an MDX query in this mode, the following additional properties are automatically included in the query:  
  
 **Member Properties** MEMBER_CAPTION, MEMBER_UNIQUE_NAME  
  
 **Cell Properties** VALUE, BACK_COLOR, FORE_COLOR, FORMATTED_VALUE, FORMAT_STRING, FONT_NAME, FONT_SIZE, FONT_FLAGS  
  
 To specify your own additional properties, you must manually edit the MDX query in Query mode.  
  
### Graphical MDX Query Designer Toolbar in Design Mode  
 The query designer toolbar provides buttons to help you design MDX queries using the graphical interface. The following table lists the buttons and their functions.  
  
|Button|Description|  
|------------|-----------------|  
|**Edit As Text**|Not enabled for this data source type.|  
|**Import**|Import an existing query from a report definition (.rdl) file on the file system. For more information, see [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).|  
|![Change to MDX query view](../../analysis-services/media/rsqdicon-commandtypemdx.gif "Change to MDX query view")|Switch to Command Type MDX.|  
|![Change to DMX query language view](../media/rsqdicon-commandtypedmx.gif "Change to DMX query language view")|Switch to Command Type DMX.|  
|![Refresh result data](../../analysis-services/media/rsqdicon-refresh.gif "Refresh result data")|Refresh metadata from the data source.|  
|![Add calculated member](../../analysis-services/media/rsqdicon-addcalculatedmember.gif "Add calculated member")|Display the **Calculated Member Builder** dialog box.|  
|![Toggle for show empty cells](../../analysis-services/media/rsqdicon-showemptycells.gif "Toggle for show empty cells")|Toggle between showing and not showing empty cells in the Data pane. (This is the equivalent to using the NON EMPTY clause in MDX).|  
|![AutoExecute the query](../../analysis-services/media/rsqdicon-autoexecute.gif "AutoExecute the query")|Automatically run the query and show the result every time a change is made. Results are shown in the Data pane.|  
|![Show Aggregations button](../../analysis-services/media/rsqdicon-showaggregations.gif "Show Aggregations button")|Show aggregations in the Data pane.|  
|![Delete](../../analysis-services/media/rsqdicon-delete.gif "Delete")|Delete the selected column in the Data pane from the query.|  
|![Icon for the Query Parameters dialog box](../../analysis-services/media/iconqueryparameter.gif "Icon for the Query Parameters dialog box")|Display the **Query Parameters** dialog box. When you specify values for a query parameter, a report parameter with the same name is automatically created. The value of the query parameter is set to an expression that references the report parameter.|  
|![Prepare Query button](../../analysis-services/media/rsqdicon-preparequery.gif "Prepare Query button")|Prepare the query.|  
|![Run the query](../../analysis-services/media/rsqdicon-run.gif "Run the query")|Run the query and display the results in the Data pane.|  
|![Cancel the query](../../analysis-services/media/rsqdicon-cancel.gif "Cancel the query")|Cancel the query.|  
|![Switch to Design mode](../../analysis-services/media/rsqdicon-designmode.gif "Switch to Design mode")|Toggle between Design mode and Query mode.|  
  
## Graphical MDX Query Designer in Query Mode  
 To change the graphical query designer to **Query** mode, click the **Design Mode** button on the toolbar.  
  
 The following figure labels the panes for Query mode.  
  
 ![Analysis Services MDX query designer, query view](../../analysis-services/media/rsqd-dsawas-mdx-querymode.gif "Analysis Services MDX query designer, query view")  
  
 The following table lists the panes in this mode:  
  
|Pane|Function|  
|----------|--------------|  
|Select Cube button (**...**)|Displays the currently selected cube.|  
|Metadata/Functions/Templates pane|Displays a hierarchical list of measures, KPIs, and dimensions defined on the selected cube.|  
|Query pane|Displays the query text.|  
|Result pane|Displays the results of running the query.|  
  
 The Metadata pane displays tabs for **Metadata**, **Functions**, and **Templates**. From the **Metadata** tab, you can drag dimensions, hierarchies, KPIs, and measures onto the MDX Query pane. From the **Functions** tab, you can drag functions onto the MDX Query pane. From the **Templates** tab, you can add MDX templates to the MDX Query pane. When you execute the query, the Result pane displays the results for the MDX query.  
  
 You can extend the default MDX query generated in Design mode to include additional member properties and cell properties. When you run the query, these values do not appear in the result set. However, they are passed back to [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] and you can use these values in a report. For more information, see Extended Field Properties for an Analysis Services Database (SSRS).  
  
### Graphical Query Designer Toolbar in Query Mode  
 The query designer toolbar provides buttons to help you design MDX queries using the graphical interface.  
  
 The toolbar buttons are identical between Design mode and Query mode, but the following buttons are not enabled for Query mode:  
  
-   **Edit As Text**  
  
-   **Add Calculated Member** (![Add calculated member](../../analysis-services/media/rsqdicon-addcalculatedmember.gif "Add calculated member"))  
  
-   **Show Empty Cells** (![Toggle for show empty cells](../../analysis-services/media/rsqdicon-showemptycells.gif "Toggle for show empty cells"))  
  
-   **AutoExecute** (![AutoExecute the query](../../analysis-services/media/rsqdicon-autoexecute.gif "AutoExecute the query"))  
  
-   **Show Aggregations** (![Show Aggregations button](../../analysis-services/media/rsqdicon-showaggregations.gif "Show Aggregations button"))  
  
## See Also  
 [Define Parameters in the MDX Query Designer for Analysis Services &#40;Report Builder and SSRS&#41;](define-parameters-in-the-mdx-query-designer-for-analysis-services.md)   
 [Create a Shared Dataset or Embedded Dataset &#40;Report Builder and SSRS&#41;](create-a-shared-dataset-or-embedded-dataset-report-builder-and-ssrs.md)   
 [Analysis Services Connection Type for DMX &#40;SSRS&#41;](analysis-services-connection-type-for-dmx-ssrs.md)   
 [RSReportDesigner Configuration File](../report-server/rsreportdesigner-configuration-file.md)   
 [Analysis Services Connection Type for MDX &#40;SSRS&#41;](analysis-services-connection-type-for-mdx-ssrs.md)  
  
  
