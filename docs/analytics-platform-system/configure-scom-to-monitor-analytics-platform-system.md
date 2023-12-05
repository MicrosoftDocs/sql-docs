---
title: Configure System Center Operations Manager to monitor APS
description: Follow these steps to configure the System Center Operations Manager (SCOM) management packs for Analytics Platform System. The Management Packs are required to monitor Analytics Platform System from Operations Manager.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/05/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Configure System Center Operations Manager (SCOM) to Monitor Analytics Platform System
Follow these steps to configure the System Center Operations Manager (SCOM) Management Packs for Analytics Platform System. The Management Packs are required to monitor Analytics Platform System from Operations Manager.  
  
## <a id="BeforeBegin"></a> Before you begin

**Prerequisites**  
  
System Center Operations Manager 2007 R2 must be installed and running.  
  
The management packs must be installed and configured. See [Install the Operations Manager Management Packs (Analytics Platform System)](install-the-scom-management-packs.md) and [Import the Operations Manager Management Pack for PDW (Analytics Platform System)](import-the-scom-management-pack-for-pdw.md).  
  
## <a id="ConfigureRunAsProfile"></a> Configure Run-As Profile in System Center
To configure System Center, you have to perform following steps:  
  
-   Create Run As account for the **APS Watcher** domain user and map it to the **Microsoft APS Watcher Account.**  
  
-   Create Run As account for the **monitoring_user** APS user and map it to the **Microsoft APS Action Account**.  
  
Here are detailed instructions on how to do the tasks:  
  
1. Create the **APS Watcher** Run As account with **Windows** account type for the **APS Watcher** domain user.  
  
    1. Navigate to the **Administration** pane, right-click on **Run As Configuration** -> **Accounts** and select **Create Run As Account...**  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/ConfigureScomCreateRunAsAccount.png" alt-text="Screenshot showing the Create Run As Account option.":::
  
    1. The **Create Run As Account Wizard** dialog opens. On the **Introduction** page, select **Next**.  
  
    1. On the **General Properties** page, select **Windows** from **Run As Account type** and specify "APS Watcher" as the **Display name**.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/CreateRunAsAccountWizardGeneralProperties.png" alt-text="Screenshot showing the General Properties page of the Create Run As Account Wizard.":::
  
    1. On the **Credentials** page,
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/CreateRunAsAccountWizardCredentials.png" alt-text="Screenshot showing the Credentials page of the Create Run As Account Wizard.":::
  
    1. On the **Distribution Security** page, select **Less secure** and select the **Create** button to finish.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/CreateRunAsAccountWizardDistributionSecurity.png" alt-text="Screenshot showing the Distribution Security page of the Create Run As Account Wizard.":::
  
        1. If you decide to use the **More secure** option, you have to manually specify computers to which credentials will be distributed. To do this, after creating the Run As account, right-click on it and select **Properties**.  
  
        1. Navigate to the **Distribution** tab and **Add** desired computers.  
  
            :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/RunAsAccountProperties.png" alt-text="Screenshot showing the Run As Account Properties dialog box.":::
  
1. Set the **Microsoft APS Watcher Account** profile to use **APS Watcher** Run As account.  
  
    1. Navigate to **Administration** -> **Run As Configuration** -> **Profiles**.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/AdministrationRunAsConfigurationProfiles.png" alt-text="Screenshot showing the Profiles option.":::
  
    1. Right-click on **Microsoft APS Watcher Account** from the list and select **Properties**.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/MicrosoftApsWatcherAccountProperties.png" alt-text="Screenshot showing the Properties option.":::
  
    1. The **Run As Profile Wizard** dialog opens. Skip the **Introduction** page by selecting **Next**.  
  
    1. On the **General Properties** page, select **Next**.  
  
    1. On the **Run As Accounts** page, select the **Add...** button and select the previously created **APS Watcher** Run As account.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/RunAsProfileWizardAdd.png" alt-text="Screenshot showing the Add a Run As Account dialog box.":::
  
    1. Select **Save** to finish profile assignment.  
  
1. Wait until APS appliances discovery completes.  
  
    1. Navigate to the **Monitoring** pane and open the **SQL Server Appliance** -> **Microsoft Analytics Platform System** -> **Appliances** state view.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/SqlServerApplianceMicrosoftApsAppliances.png" alt-text="Screenshot showing the Appliances option.":::
  
    1. Wait until the appliance appears in the list. The name of the appliance should be equal to one specified in the registry. After discovery completes, you should see all appliances listed but not monitored. To enable monitoring, follow the next steps.  
  
    > [!NOTE]  
    > The next steps can be completed in parallel while you are waiting for the initial appliance discovery to finish.  
  
1. Create another new Run As account to query APS for health data retrieval.  
  
    1. Begin creating a new Run As account as described in step 1.  
  
    1. On the **General Properties** page, select **Basic Authentication** account type.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/CreateRunAsAccountWizardGeneralProperties2.png" alt-text="Screenshot showing the General Properties page of the Create Run As Account Wizard with Basic Authentication selected from the Run As Account type dropdown.":::
  
    1. On the **Credentials** page, supply valid credentials to access APS health state DMVs.  
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/CreateRunAsAccountWizardCredentials2.png" alt-text="Screenshot showing the Credentials page of the Create Run As Account Wizard with valid credentials to access APS health state DMVs.":::
  
1. Configure the **Microsoft APS Action Account** profile to use the newly created Run As account for the APS instance.  
  
    1. Navigate to the **Microsoft APS Action Account** properties as described in step 2.  
  
    1. On the **Run As Accounts** page, select **Add...**.
    1. Select the newly created Run As account.
  
        :::image type="content" source="./media/configure-scom-to-monitor-analytics-platform-system/RunAsProfileWizardAdd2.png" alt-text="Screenshot showing the Add a Run As Account dialog box with APS Action selected from the Run As account dropdown list.":::
  
## Related content

- [Monitor with System Center Operations Manager - Analytics Platform System](monitor-the-appliance-by-using-system-center-operations-manager.md)
- [What's new in Analytics Platform System, a scale-out MPP data warehouse](whats-new-analytics-platform-system.md)
