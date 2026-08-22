---
title: Appliance install and configure for APS
description: As an Analytics Platform System appliance administrator, learn about the initial steps to set up and get started using your new appliance.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: martinle
ms.date: 06/23/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
ms.custom: kr2b-contr-experiment
---

# Appliance installation and configuration for Analytics Platform System

This article walks Analytics Platform System (APS) appliance administrators through the initial steps to set up and get started using your new appliance.

<!-- MISSING LINKS ## <a name="BeforeYouBegin"></a>Before You Begin
Before you begin to install, configure, and use your new appliance, we recommend reviewing information about the appliance components. Review the following to familiarize yourself with the appliance:

- Review [Understanding the Appliance Nodes and Hardware (SQL Server PDW)](assetId:///f60f419f-d1e1-403d-8cf9-07e7ef6d6627) to be sure you understand the components included in your new appliance.

- Review [Connecting to SQL Server PDW (SQL Server PDW)](assetId:///721851d5-e521-4d5b-ba6d-8e2e9d3c7808) to understand how and when appliance administrators will connect to each appliance node.
-->

## <a name="InstallHardware"></a>Install the hardware

Your new appliance will be delivered on pallets to the dock at your data center.

> [!IMPORTANT]
>
> In some cases, your independent hardware vendor unpacks, racks, and connects the appliance for you in your data center. If so, these instructions are not necessary and you can skip to the [Configure the Appliance](#ConfigureAppliance) section below.

If your independent hardware vendor (IHV) isn't performing the hardware install, use the following steps to install the hardware.

|Task|Description|
|-|-|
|Confirm documentation|Confirm that you've received all necessary documents and information from your independent hardware vendor. See [Information to Obtain from Your IHV (Analytics Platform System)](information-to-obtain-from-your-ihv.md).|
|Install hardware|Verify the data center can accommodate the appliance. Move the appliance components to the data center. Rack the network switches, PDUs, and cabling. See [Hardware Installation (Analytics Platform System)](hardware-installation.md).|

## <a name="PowerOnAppliance"></a>Power on the appliance

|Task|Description|
|-|-|
|Power on the appliance|Power on each appliance component node in the required order, waiting as necessary to confirm that no errors are encountered.|

## <a name="ConfigureAppliance"></a>Configure the appliance

|Task|Description|
|-|-|
|Configure the appliance by using the SQL Server PDW **Configuration Manager**|Use the Configuration Manager to set appliance passwords, time zones, network and firewall settings, security certificates, and performance and other settings on your appliance. See [Appliance Configuration (Analytics Platform System)](appliance-configuration.md).|

> [!WARNING]
> Configuration changes should only be made using the SQL Server PDW **Configuration Manager**. Changes not exposed through **Configuration Manager** are not supported. For example, the SQL Server PDW appliance only supports the US-English language setting.

## <a name="SoftwareServicing"></a>Set up software servicing

|Task|Description|
|-|-|
|Apply SQL Server PDW updates|(Optional) You might need to apply one or more SQL Server PDW updates to update your SQL Server PDW software to the latest version. See [Apply Analytics Platform System Hotfixes (Analytics Platform System)](apply-analytics-platform-system-hotfixes.md).|
|Configure Windows Server Update Services|Configure the appliance to receive updates from Windows Server Update Services for supporting software. See [Download and Apply Microsoft Updates (Analytics Platform System)](download-and-apply-microsoft-updates.md).|

## Related content

- [Monitor the appliance with the Admin Console - Analytics Platform System](monitor-the-appliance-by-using-the-admin-console.md)
- [Monitor Appliance Health State (Analytics Platform System)](../relational-databases/system-dynamic-management-views/sys-dm-pdw-component-health-status-transact-sql.md)
