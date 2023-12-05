---
title: Import System Center Operations Manager Management Pack
description: Follow these steps to import the System Center Operations Manager (SCOM) Management Packs for Analytics Platform System (APS). The management packs are required to monitor Parallel Data Warehouse from Operations Manager.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/05/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
---

# Import the System Center Operations Manager Management Pack - Analytics Platform System
Follow these steps to import the System Center Operations Manager (SCOM) Management Packs for Analytics Platform System (APS). The management packs are required to monitor Parallel Data Warehouse from Operations Manager. 
  
## <a id="BeforeBegin"></a> Before you begin

**Prerequisites**  
  
System Center Operations Manager 2007 R2 must be installed and running.  
  
The management packs must be installed. See [Install the System Center Operations Manager Management Packs (Analytics Platform System)](install-the-scom-management-packs.md).  
  
## <a id="Step1"></a> Step 1: Import the SQL Server Appliance Base Management Pack
  
1. Sign in the computer with an account that is a member of the Operations Manager Administrators role for the Operations Manager 2007 management group.  
  
1. In the Operations console, select **Administration**.  
  
1. Right-click the **Management Packs** node, and then select **Import Management Packs**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_IMP.png" alt-text="Screenshot where to select Import Management Packs.":::
  
1. In the list of management packs, select the management pack that you want to import, select **Select**, and then select **Add**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_IMP2.png" alt-text="Screenshot of the Management pack list.":::
  
1. Search for **Appliance** and then drill down into SQL Server Appliance Base and then select **Add** the two choices.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_IMP3.png" alt-text="Screenshot of the SQL Server appliance base.":::
  
1. Once the two Management Packs were in the bottom selected pane, then select **OK**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_IMP4.png" alt-text="Screenshot of the management packs from catalog page.":::
  
1. Select **Install**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_IMP5.png" alt-text="Screenshot showing the Import Management Packs wizard on the Select Management Packs step with the Install option circled in red.":::
  
1. Once complete, select **Close**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_IMP6.png" alt-text="Screenshot of the complete page. Close is circled in red.":::
  
## <a id="Step2"></a> Import the Monitoring Pack for Microsoft SQL Server 2008 R2 Parallel Data Warehouse Appliance
  
1. Right-click the **Management Packs** node, and then select **Import Management Packs**.  
  
1. Choose **Add from disk**....  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_PDW.png" alt-text="Screenshot of where to right-click Management Packs.":::
  
1. Go to the location where you extracted the SQL Server PDW Management Packs and choose the three management packs that are in the **Selected Management packs to import** section. You can do this by selecting the first one, selecting Shift, and selecting the last one. Once they are all selected, select **Open**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_PDW2.png" alt-text="Screenshot of the management packs to import page.":::
  
1. Select **Install**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_PDW3.png" alt-text="Screenshot of the Import Management Packs wizard on the Select Management Packs step with the Install option circled in red.":::
  
1. Select **Close**.  
  
    :::image type="content" source="./media/import-the-scom-management-pack-for-pdw/SCOM_PDW4.png" alt-text="A screenshot of the completed screen. Close is circled in red.":::
  
## Related content

- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)

## Next step

> [!div class="nextstepaction"]
> [Configure System Center Operations Manager (SCOM) to Monitor Analytics Platform System](configure-scom-to-monitor-analytics-platform-system.md)
