---
title: "Spatial Results Window | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.technology: scripting
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: c2d5a477-6496-4d01-adee-7322ebdfadf3
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Spatial Results Window
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The **Spatial results** window provides visual mapping tools for viewing spatial data. To view spatial results, your query results must include a spatial column with either geometry or geography data.  
  
> [!NOTE]  
>  The **Spatial results** window is only available if your results are returned to a grid in the **Results** window. If you specify that your results are returned as text, this window is not available.  
  
## Options  
 **Select spatial column**  
 Specify the spatial column you want to view from the spatial columns in the query results. Only one column can be selected at a time.  
  
 **Select label column**  
 Specify the non-spatial column from the columns returned in the query results to label the spatial data. Only one column can be selected at a time.  
  
 This option is not available when only point instances are returned in a query.  
  
 **Select projection**  
 Display geography data in one of four projections: Equirectangular, Mercator, Robinson, or Bonne.  
  
 This option is not available for geometry data.  
  
 **Zoom**  
 Adjust the map display on an exponential scale.  
  
 **Show grid lines**  
 Toggle coordinate gridlines on or off.  
  
 For polygon shapes, the label is displayed only when the shape is large enough to hold the label text. To display labels for small shapes, adjust the zoom.  
  
> [!NOTE]  
>  Point instances cannot be labeled.  
  
## See Also  
 [View Spatial Data in Object Explorer](../../relational-databases/scripting/view-spatial-data-in-object-explorer.md)   
 [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md)   
 [Database Engine Query Editor &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/database-engine-query-editor-sql-server-management-studio.md)   
 [Query and Text Editors &#40;SQL Server Management Studio&#41;](../../relational-databases/scripting/query-and-text-editors-sql-server-management-studio.md)  
  
  
