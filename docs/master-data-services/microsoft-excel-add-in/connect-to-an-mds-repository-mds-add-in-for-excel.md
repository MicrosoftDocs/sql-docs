---
title: Connect to an MDS Repository
description: In the Master Data Services Add-in for Excel, you must connect to a Master Data Services repository before you can load or publish data.
ms.custom: microsoft-excel-add-in
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: 8f427312-4c09-4c8b-b9f9-8b235557a74b
author: CordeliaGrey
ms.author: jiwang6
---
# Connect to an MDS Repository (MDS Add-in for Excel)

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you must connect to an MDS repository before you can load or publish data.  
  
## Prerequisites  
 To perform this procedure:  
  
-   You must have permission to access the **Explorer** functional area.  
  
### To connect to an MDS repository  
  
1.  In the MDS [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], on the **Master Data** tab, in the **Connect and Load** group, click the arrow under the **Connect** button and click **Manage Connections**.  
  
2.  On the **Manage Connections** dialog box, in the **New connection** section, click **Create a new connection**.  
  
3.  Click **New**.  
  
4.  On the **Add New Connection** dialog box, in the **Description** field, type a description for your connection. This connection will be displayed when you click the arrow under the **Connect** button on the toolbar.  
  
5.  In the **MDS server address** box, type the URL of the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, for example `https://contoso/mds`.  
  
    > [!NOTE]  
    >  Ensure that you use the computer name; do not use "localhost".  
  
6.  Click **OK**. The name is displayed in the **Existing Connections** section.  
  
7.  Optionally, click **Test** to test the connection. A confirmation or error dialog is displayed. Click **OK** to close it.  
  
8.  Click **Connect**. The **Master Data Services** pane is displayed.  
  
## Next Steps  
  
-   [Export Data to Excel from Master Data Services](../../master-data-services/microsoft-excel-add-in/export-data-to-excel-from-master-data-services.md)  
  
-   [Filter Data before Exporting &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/filter-data-before-exporting-mds-add-in-for-excel.md)  
  
## See Also  
 [Connections &#40;MDS Add-in for Excel&#41;](../../master-data-services/microsoft-excel-add-in/connections-mds-add-in-for-excel.md)  
  
  
