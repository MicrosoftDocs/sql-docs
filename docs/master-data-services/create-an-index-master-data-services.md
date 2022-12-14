---
description: "Create an Index (Master Data Services)"
title: Create an Index
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: d694a105-69b1-4ff6-99d3-1f408b916b81
author: CordeliaGrey
ms.author: jiwang6
---
# Create an Index (Master Data Services)

[!INCLUDE [SQL Server - Windows only ASDBMI  ](../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  Create a custom index on a list of attributes that you query frequently, to improve query performance.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the System Administration functional area. For more information, see [Functional Area Permissions &#40;Master Data Services&#41;](../master-data-services/functional-area-permissions-master-data-services.md).  
  
-   You must be a model administrator. For more information, see [Administrators &#40;Master Data Services&#41;](../master-data-services/administrators-master-data-services.md).  
  
 **To create an index**  
  
1.  In Master Data Manager, click **System Administration**.  
  
2.  On the **Manage Model** page, select a model from the grid and click then **Entities**.  
  
3.  On the **Manage Entity** page, from the **grid** , select the row for the entity that you want to create an index for.  
  
4.  Click **Indexes**.  
  
5.  In the **Name** box, type a name for the index.  
  
6.  Select **Is Unique** if you want to create a unique index. For more information about index types, see [Custom Index &#40;Master Data Services&#41;](../master-data-services/custom-index-master-data-services.md).  
  
7.  Click attributes in the **Available Attributes** box, and then click the **Add** arrow. To add all attributes, click the **Add All** arrow.  
  
8.  Click **Save**.  
  
 For each created index, a row with four columns is added to the grid. The following table describes the columns.  
  
|Column Name|Description|  
|-----------------|-----------------|  
|Status|The index status.<br /><br /> When you click **Save**, the ![Icon for updating status](../master-data-services/media/mds-statusicon-updating.png "Icon for updating status") image displays indicating that the index is updating.<br /><br /> If there are errors when creating or editing an index, the ![Icon for error status](../master-data-services/media/mds-statusicon-error.png "Icon for error status") image displays.<br /><br /> Otherwise, the status is OK, and the ![Icon for OK status](../master-data-services/media/mds-statusicon-ok.png "Icon for OK status") image displays.|  
|Name|The index name.|  
|Is Unique|Specifies whether the index is unique.|  
|On Attributes|Shows the display names  of attributes that the index is defined on.|  
  
 When you click an index, the following information displays.  
  
-   **Created By**: The name of the user who created the index.  
  
-   **On**: The date and time when the index was created.  
  
-   **Updated By**: The name of the user who last updated the index.  
  
-   **On**: The date and time when the index was last updated.  
  
## Next Steps  
 [Edit and Delete an Index &#40;Master Data Services&#41;](../master-data-services/edit-and-delete-an-index-master-data-services.md)  
  
## See Also  
 [Custom Index &#40;Master Data Services&#41;](../master-data-services/custom-index-master-data-services.md)  
  
  
