---
title: "Import the SCOM Management Pack for PDW (Analytics Platform System)"
ms.custom: na
ms.date: 01/05/2017
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: fa735041-8e58-4886-ae3b-36f3c6298b12
caps.latest.revision: 6
author: BarbKess
---
# Import the SCOM Management Pack for PDW
Follow these steps to import the System Center Operations Manager (SCOM) Management Packs for SQL Server PDW. The Management Packs are required to monitor SQL Server PDW from SCOM.  
  
## <a name="BeforeBegin"></a>Before You Begin  
**Prerequisites**  
  
System Center Operations Manager 2007 R2 must be installed and running.  
  
The management packs must be installed. See [Install the SCOM Management Packs &#40;Analytics Platform System&#41;](install-the-scom-management-packs.md).  
  
## <a name="Step1"></a>Step 1: Import the SQL Server Appliance Base Management Pack  
  
1.  Log on to the computer with an account that is a member of the Operations Manager Administrators role for the Operations Manager 2007 management group.  
  
2.  In the Operations console, click **Administration**.  
  
3.  Right-click the **Management Packs** node, and then click **Import Management Packs**.  
  
    ![Click Import Management Packs](./media/import-the-scom-management-pack-for-pdw/SCOM_IMP.png "SCOM_IMP")  
  
4.  In the list of management packs, select the management pack that you want to import, click **Select**, and then click **Add**.  
  
    ![Management pack list](./media/import-the-scom-management-pack-for-pdw/SCOM_IMP2.png "SCOM_IMP2")  
  
5.  Search for **Appliance** and then drill down into SQL Server Appliance Base and then click **Add** the two choices.  
  
    ![SQL Server appliance base](./media/import-the-scom-management-pack-for-pdw/SCOM_IMP3.png "SCOM_IMP3")  
  
6.  Once the two Management Packs were in the bottom selected pane, then click **OK**.  
  
    ![Select both management packs](./media/import-the-scom-management-pack-for-pdw/SCOM_IMP4.png "SCOM_IMP4")  
  
7.  Click **Install**.  
  
    ![Click Install](./media/import-the-scom-management-pack-for-pdw/SCOM_IMP5.png "SCOM_IMP5")  
  
8.  Once Complete, click **Close**.  
  
    ![Once complete, click Close](./media/import-the-scom-management-pack-for-pdw/SCOM_IMP6.png "SCOM_IMP6")  
  
## <a name="Step2"></a>Import the Monitoring Pack for Microsoft SQL Server 2008 R2 Parallel Data Warehouse Appliance  
  
1.  Right-click the **Management Packs** node, and then click **Import Management Packs**.  
  
2.  Choose **Add from disk**….  
  
    ![Right-click Management Packs](./media/import-the-scom-management-pack-for-pdw/SCOM_PDW.png "SCOM_PDW")  
  
3.  Go to the location where you extracted the SQL Server PDW Management Packs and choose the three management packs that are in the "Selected Management packs to import" section. You can do this by selecting the first one, clicking Shift, and selecting the last one. Once they are all selected, click **Open**.  
  
    ![Select management packs](./media/import-the-scom-management-pack-for-pdw/SCOM_PDW2.png "SCOM_PDW2")  
  
4.  Click **Install**.  
  
    ![Click Install](./media/import-the-scom-management-pack-for-pdw/SCOM_PDW3.png "SCOM_PDW3")  
  
5.  Click **Close**.  
  
    ![Click Close](./media/import-the-scom-management-pack-for-pdw/SCOM_PDW4.png "SCOM_PDW4")  
  
## Next Step  
Now that you have imported the Management Packs, continue to the next step: [Configure SCOM to Monitor Analytics Platform System &#40;Analytics Platform System&#41;](configure-scom-to-monitor-analytics-platform-system.md).  
  
<!-- MISSING LINKS ## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  -->  
  
