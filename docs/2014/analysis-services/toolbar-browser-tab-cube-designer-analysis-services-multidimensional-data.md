---
title: "Toolbar (Browser Tab, Cube Designer) (Analysis Services - Multidimensional Data) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: a1c6272d-e514-456b-9995-b73fec0112a2
author: minewiskan
ms.author: owend
manager: craigg
---
# Toolbar (Browser Tab, Cube Designer) (Analysis Services - Multidimensional Data)
  Use the features in the **Toolbar** in Cube Designer to perform common operations while designing or browsing a cube or its object, or when creating an MDX query. Operations that are common to both design time and query view include setting the user context, processing objects, and setting the default language.  
  
 The following table lists the **Toolbar** buttons and their functions.  
  
|Button|Description|  
|------------|-----------------|  
|**Edit As Text**|Not enabled for this data source type.|  
|**Import**|Import an existing query from a report definition (.rdl) file on the file system.|  
|![Change to MDX query view](media/rsqdicon-commandtypemdx.gif "Change to MDX query view")|Switch to Command Type MDX.|  
|![Refresh result data](media/rsqdicon-refresh.gif "Refresh result data")|Refresh metadata from the data source.|  
|![Add calculated member](media/rsqdicon-addcalculatedmember.gif "Add calculated member")|Display the **Calculated Member Builder** dialog box.|  
|![Toggle for show empty cells](media/rsqdicon-showemptycells.gif "Toggle for show empty cells")|Toggle between showing and not showing empty cells in the Data pane. (This is the equivalent to using the NON EMPTY clause in MDX).|  
|![AutoExecute the query](media/rsqdicon-autoexecute.gif "AutoExecute the query")|Automatically run the query and show the result every time a change is made. Results are shown in the Data pane.|  
|![Show Aggregations button](media/rsqdicon-showaggregations.gif "Show Aggregations button")|Show aggregations in the Data pane.|  
|![Delete](media/rsqdicon-delete.gif "Delete")|Delete the selected column in the Data pane from the query.|  
|![Icon for the Query Parameters dialog box](media/iconqueryparameter.gif "Icon for the Query Parameters dialog box")|Display the **Query Parameters** dialog box. When you specify values for a query parameter, a parameter with the same name is automatically created.|  
|![Prepare Query button](media/rsqdicon-preparequery.gif "Prepare Query button")|Prepare the query.|  
|![Run the query](media/rsqdicon-run.gif "Run the query")|Run the query and display the results in the Data pane.|  
|![Cancel the query](media/rsqdicon-cancel.gif "Cancel the query")|Cancel the query.|  
|![Switch to Design mode](media/rsqdicon-designmode.gif "Switch to Design mode")|Toggle between Design mode and Query mode.|  
  
 In general, the toolbar buttons are the same for **Design Mode** and **Query Mode**. However, the following buttons are not enabled for Query mode:  
  
-   **Edit As Text**  
  
-   **Add Calculated Member** (![Add calculated member](media/rsqdicon-addcalculatedmember.gif "Add calculated member"))  
  
-   **Show Empty Cells** (![Toggle for show empty cells](media/rsqdicon-showemptycells.gif "Toggle for show empty cells"))  
  
-   **AutoExecute** (![AutoExecute the query](media/rsqdicon-autoexecute.gif "AutoExecute the query"))  
  
-   **Show Aggregations** (![Show Aggregations button](media/rsqdicon-showaggregations.gif "Show Aggregations button"))  
  
## Options  
  
|Option|Description|  
|------------|-----------------|  
|**Process**|Click to display the **Process** dialog box and process the cube. For more information about the **Process** dialog box, see [Process Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](process-dialog-box-analysis-services-multidimensional-data.md).|  
|**Change User**|Click to display the **Security Context** dialog box and change the user and role used on the **Browser** tab. For more information about the **Security Context** dialog box, see [Security Context Dialog Box &#40;Analysis Services - Multidimensional Data&#41;](security-context-dialog-box-analysis-services-multidimensional-data.md).|  
|**Reconnect**|Click to reconnect the **Calculations** tab to the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] instance and database that contains the cube, if the session for the **Browser** tab is disconnected due to connection loss or timeout.|  
|**Refresh**|Click to refresh the **Metadata** and **Reports** panes.|  
|**Sort Ascending**|Click to sort the siblings of the selected row in the **Report** pane in ascending order for the language specified in **Language**.<br /><br /> **Note** This option is enabled only if a cell in the **Reports** pane is selected.|  
|**Sort Descending**|Click to sort the siblings of the selected row in the **Report** pane in descending order for the language specified in **Language**.<br /><br /> Note: This option is enabled only if a cell in the **Reports** pane is selected.|  
|**Auto Filter**|Click to automatically filter the results in the **Result** pane.|  
|**Show Only Top/Bottom**|Select a value or percentage to show only the topmost or bottommost number or percentage of cells in the **Report** pane based on the selected measure.<br /><br /> For more information about this option, see [TopCount &#40;MDX&#41;](/sql/mdx/topcount-mdx), [TopPercent &#40;MDX&#41;](/sql/mdx/toppercent-mdx), [BottomCount &#40;MDX&#41;](/sql/mdx/bottomcount-mdx), and [BottomPercent &#40;MDX&#41;](/sql/mdx/bottompercent-mdx).|  
|**Subtotal**|Click to display subtotals.|  
|**All Item Totals**|Click to display the totals for All members in the **Report** pane.|  
|**Show Empty Cells**|Click to display empty cells in the **Report** pane.|  
|**Clear Results**|Click to clear the results in the **Report** pane.|  
|**Commands and Options**|Click to display the **Commands and Options** dialog box and edit advanced properties for the Microsoft Office 11.0 PivotTable control in the **Report** pane. For more information about the **Commands and Options** dialog box, see the Microsoft Office documentation.|  
|**Perspective**|Select the perspective from which to view data and metadata in the **Metadata** and **Report** panes.<br /><br /> To view the cube without using a perspective, select the cube name.|  
|**Language**|Select the language with which to view data and metadata in the **Metadata** and **Report** panes.<br /><br /> To view the cube using the default language, select **Default**.|  
  
  
