---
title: "XML Indexes Dialog Box (Visual Database Tools) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
f1_keywords: 
  - "vdt.dlgbox.xmlindexes"
ms.assetid: eef38310-4498-4ccc-bb77-5bbd1c7cc477
author: "markingmyname"
ms.author: "maghan"
manager: craigg

---
# XML Indexes Dialog Box (Visual Database Tools)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
Use the **XML Indexes** dialog box to create indexes for columns of the data type XML, which cannot be indexed using the **Index/Keys** dialog box. Each XML column can have more than one XML Index, but the first one created (primary) will be the basis of the others (secondary). If you delete the primary XML index, the secondary indexes will also be deleted.  
  
## Options  
**Selected XML Index**  
Lists existing XML indexes. Select to show its properties in the grid to the right. If the list is empty, none have been defined for the table.  
  
**Add**  
Create a new XML index.  
  
**Delete**  
Delete XML index selected in the **Selected XML Index** list. If you delete the primary XML index, you will be notified that this will delete all secondary ones as well, and you can either continue or cancel the action.  
  
**General Category**  
When expanded, shows the property fields for **Columns**, **Is Primary**, and **Type**.  
  
**Columns**  
Shows that this index is sorted in ascending order.  
  
**Is Primary**  
Indicates whether this is the primary index. The first XML index created on the column will be the basis of the others.  
  
**Primary reference name**  
Shows the name of the primary index if this is a secondary index. Only available if this is a secondary index.  
  
**Secondary type**  
Shows the type of secondary index. Only available if this is a secondary index.  
  
**Type**  
Shows that this is an XML index.  
  
**Identity Category**  
When expanded, shows the **Name** and **Description** property fields.  
  
**Name**  
Shows the name of the XML index. When a new one is created, it is given a default name based on the table in the active window in Table Designer. You can change the name at any time.  
  
**Description**  
Describe the index. To write a more detailed description, click **Description** and then click the ellipsis button (**...**) that appears to the right of the property field. This provides a larger area in which to write text.  
  
**Table Designer Category**  
When expanded, shows information about the properties of this XML index.  
  
**Fill Specification**  
When expanded, shows information for **Fill Factor** and **Pad Index**.  
  
**Fill Factor**  
Specify what percentage of the index page the system can fill. Once a page is full, the system must split the page if new data is added, which impairs performance.  
  
-   A value of 100 means the pages will be full; this requires the least amount of storage space but is the least efficient. This setting should be used only when there will be no changes to the data, for example, on a read-only table.  
  
-   A lower value leaves more empty space on the data pages, which reduces the need to split data pages as indexes grow. However, it requires more storage space. This setting is more appropriate when there will be changes to the data in the table.  
  
**Pad Index**  
Provide pages in this index the same percentage of empty space (padding) that is specified in **Fill Factor**.  
  
**Is Disabled**  
Specify whether this index is disabled. Disabled indexes do not support searches, nor do they get updated when new items are added to the table. You can improve performance for bulk inserts and updates by disabling an index.  
  
**Page Locks Allowed**  
Specify whether page-level locking is allowed on this index. Allowing or disallowing page-level locking affects database performance.  
  
**Re-compute Statistics**  
Compute new statistics when the index is created. Re-computing statistics slows the building of indexes but usually improves query performance.  
  
**Row Locks Allowed**  
Specify whether row-level locking is allowed on this index. Allowing or disallowing row-level locking affects database performance.  
  
## See Also  
[Create XML Indexes](../../relational-databases/xml/create-xml-indexes.md)  
  
