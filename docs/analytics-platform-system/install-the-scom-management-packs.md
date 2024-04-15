---
title: Install System Center Operations Manager management packs
description: Follow these steps to download and install the System Center Operations Manager (SCOM) Management Packs for SQL Server PDW. The Management Packs are required to monitor SQL Server PDW from SCOM.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/05/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom:
  - intro-installation
---

# Install SQL Server Operations Manager (SCOM) management packs for Analytics Platform System
Follow these steps to download and install the System Center Operations Manager (SCOM) Management Packs for SQL Server PDW. The Management Packs are required to monitor SQL Server PDW from Operations Manager.  
  
## <a id="BeforeBegin"></a> Before you begin

**Prerequisites**  
  
System Center Operations Manager must be installed and running. SQL Server PDW 2012 requires System Center Operations Manager 2007 R2, System Center Operations Manager 2012, or System Center Operations Manager 2012 service pack 1.  
  
## <a id="Step1"></a> Step 1: Download the Management Packs

For the APS PDW workload, download the [System Center Management Pack for the Microsoft Analytics Platform System](https://go.microsoft.com/fwlink/?LinkId=396857).  
  
For the appliance management, download the [SQL Server Appliance Base Management Pack](/previous-versions/system-center/packs/gg602398(v=technet.10)).  
  
For older versions of PDW without APS, download the [System Center Monitoring Pack for Microsoft SQL Server 2012 Parallel Data Warehouse Appliance](./download-and-apply-microsoft-updates.md?view=aps-pdw-2016-au7&preserve-view=true).  
  
<!-- MISSING LINKS - For the HDInsight workload, download the [System Center Management Pack for HDInsight](https://go.microsoft.com/fwlink/?LinkId=390208).  -->
  
## <a id="Step2"></a> Step 2: Install the Management Packs
  
### Install the SQL Server Appliance Base Management Pack
  
1. To run the install, double-click on the downloaded SQL Server Appliance Base Management Pack.  
  
1. Accept the License Agreement, and select **Next**.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agrmt.png" alt-text="A screenshot of the License Agreement.":::
  
1. Select your own installation folder, or use the default Management Pack Installation Folder.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agrmt2.png" alt-text="A screenshot of where to select Installation Folder.":::
  
1. Select **Install**.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agrmt3.png" alt-text="Screenshot of the SQL Server Appliance Base Monitoring MP Installer wizard on the Confirm Installation step with the Install option circled in red.":::
  
1. Select **Close**.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agrmt4.png" alt-text="A screenshot of the installation complete page.":::
  
### Install the Monitoring Pack for SQL Server PDW Appliance
  
1. To run the install, double-click on the downloaded SQL Server PDW Appliance Management Pack.  
  
1. Accept the License Agreement, and select **Next**.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agmtB.png" alt-text="A screenshot of the license agreement.":::
  
1. Choose the directory that will hold the extracted files. The default Management Pack installation folder is shown by default. Select the default, or select your own installation folder.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agmtB1.png" alt-text="A screenshot of the installation folder.":::
  
1. Select **Install**.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agmtB2.png" alt-text="Screenshot of the PDWMP Installer wizard on the Confirm Installation step with the Install option circled in red.":::
  
1. Select **Close**.  
  
    :::image type="content" source="./media/install-the-scom-management-packs/SCOM_licnse_agmtB3.png" alt-text="A screenshot of the final installation complete page.":::
  
## Related content

- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)

## Next step

> [!div class="nextstepaction"]
> [Import the SCOM Management Pack - Analytics Platform System](import-the-scom-management-pack-for-pdw.md)
