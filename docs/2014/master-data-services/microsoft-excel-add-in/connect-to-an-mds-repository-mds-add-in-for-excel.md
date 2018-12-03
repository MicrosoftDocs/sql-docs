---
title: "Connect to an MDS Repository (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 8f427312-4c09-4c8b-b9f9-8b235557a74b
author: leolimsft
ms.author: lle
manager: craigg
---
# Connect to an MDS Repository (MDS Add-in for Excel)
  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you must connect to an MDS repository before you can load or publish data.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
### To connect to an MDS repository  
  
1.  In the MDS [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], on the **Master Data** tab, in the **Connect and Load** group, click the arrow under the **Connect** button and click **Manage Connections**.  
  
2.  On the **Manage Connections** dialog box, in the **New connection** section, click **Create a new connection**.  
  
3.  Click **New**.  
  
4.  On the **Add New Connection** dialog box, in the **Description** field, type a description for your connection. This connection will be displayed when you click the arrow under the **Connect** button on the toolbar.  
  
5.  In the **MDS server address** box, type the URL of the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, for example http://contoso/mds.  
  
    > [!NOTE]  
    >  Ensure that you use the computer name; do not use "localhost".  
  
6.  Click **OK**. The name is displayed in the **Existing Connections** section.  
  
7.  Optionally, click **Test** to test the connection. A confirmation or error dialog is displayed. Click **OK** to close it.  
  
8.  Click **Connect**. The **Master Data Services** pane is displayed.  
  
## Next Steps  
  
-   [Load Data from MDS into Excel](export-data-to-excel-from-master-data-services.md)  
  
-   [Filter Data before Loading &#40;MDS Add-in for Excel&#41;](filter-data-before-exporting-mds-add-in-for-excel.md)  
  
## See Also  
 [Connections &#40;MDS Add-in for Excel&#41;](connections-mds-add-in-for-excel.md)  
  
  
