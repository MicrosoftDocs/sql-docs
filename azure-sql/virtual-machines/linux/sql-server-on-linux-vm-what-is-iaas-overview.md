---
title: Overview of SQL Server on Azure Virtual Machines for Linux| Microsoft Docs
description: Learn about how to run full SQL Server editions on Azure Virtual Machines for Linux. Get direct links to all Linux SQL Server VM images and related content.
author: MashaMSFT
ms.author: mathoma
ms.date: 10/26/2021
ms.service: virtual-machines-sql
ms.subservice: service-overview
ms.topic: overview
tags: azure-service-management
---
# Overview of SQL Server on Linux Azure Virtual Machines
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

> [!div class="op_single_selector"]
> * [Windows](../windows/sql-server-on-azure-vm-iaas-what-is-overview.md)
> * [Linux](sql-server-on-linux-vm-what-is-iaas-overview.md)

SQL Server on Azure Virtual Machines enables you to use full versions of SQL Server in the cloud without having to manage any on-premises hardware. SQL Server VMs also simplify licensing costs when you pay as you go.

Azure virtual machines run in many different [geographic regions](https://azure.microsoft.com/regions/) around the world. They also offer a variety of [machine sizes](/azure/virtual-machines/sizes). The virtual machine image gallery allows you to create a SQL Server VM with the right version, edition, and operating system. This makes virtual machines a good option for a many different SQL Server workloads. 

If you're new to Azure SQL, check out the *SQL Server on Azure VM Overview* video from our in-depth [Azure SQL video series](/shows/Azure-SQL-for-Beginners?WT.mc_id=azuresql4beg_azuresql-ch9-niner):
> [!VIDEO https://learn.microsoft.com/shows/Azure-SQL-for-Beginners/SQL-Server-on-Azure-VM-Overview-4-of-61/player]

## <a id="create"></a> Get started with SQL Server VMs

To get started, choose a SQL Server virtual machine image with your required version, edition, and operating system. The following sections provide direct links to the Azure portal for the SQL Server virtual machine gallery images.

> [!TIP]
> For more information about how to understand pricing for SQL Server images, see [the pricing page for Linux VMs running SQL Server](https://azure.microsoft.com/pricing/details/virtual-machines/linux/).

| Version | Operating system | Edition |
| --- | --- | --- |
| **SQL Server 2019** | Ubuntu 18.04 | [Enterprise](https://portal.azure.com/#create/microsoftsqlserver.sql2019-ubuntu1804enterprise-ARM), [Standard](https://portal.azure.com/#create/microsoftsqlserver.sql2019-ubuntu1804standard-ARM), [Web](https://portal.azure.com/#create/microsoftsqlserver.sql2019-ubuntu1804web-ARM), [Developer](https://portal.azure.com/#create/microsoftsqlserver.sql2019-ubuntu1804sqldev-ARM) | 
| **SQL Server 2019** | Red Hat Enterprise Linux (RHEL) 8 | [Enterprise](https://portal.azure.com/#create/microsoftsqlserver.sql2019-rhel8enterprise-ARM), [Standard](https://portal.azure.com/#create/microsoftsqlserver.sql2019-rhel8standard-ARM), [Web](https://portal.azure.com/#create/microsoftsqlserver.sql2019-rhel8web-ARM), [Developer](https://portal.azure.com/#create/microsoftsqlserver.sql2019-rhel8sqldev-ARM)|
| **SQL Server 2019** | SUSE Linux Enterprise Server (SLES) v12 SP5 | [Enterprise](https://portal.azure.com/#create/microsoftsqlserver.sql2019-sles12sp5enterprise-ARM), [Standard](https://portal.azure.com/#create/microsoftsqlserver.sql2019-sles12sp5standard-ARM), [Web](https://portal.azure.com/#create/microsoftsqlserver.sql2019-sles12sp5web-ARM), [Developer](https://portal.azure.com/#create/microsoftsqlserver.sql2019-sles12sp5sqldev-ARM)|
| **SQL Server 2017** | Red Hat Enterprise Linux (RHEL) 7.4 |[Enterprise](https://portal.azure.com/#create/Microsoft.SQLServer2017EnterpriseonRedHatEnterpriseLinux74), [Standard](https://portal.azure.com/#create/Microsoft.SQLServer2017StandardonRedHatEnterpriseLinux74), [Web](https://portal.azure.com/#create/Microsoft.SQLServer2017WebonRedHatEnterpriseLinux74), [Express](https://portal.azure.com/#create/Microsoft.FreeSQLServerLicenseSQLServer2017ExpressonRedHatEnterpriseLinux74), [Developer](https://portal.azure.com/#create/Microsoft.FreeSQLServerLicenseSQLServer2017DeveloperonRedHatEnterpriseLinux74) |
| **SQL Server 2017** | SUSE Linux Enterprise Server (SLES) v12 SP2 |[Enterprise](https://portal.azure.com/#create/Microsoft.SQLServer2017EnterpriseonSLES12SP2), [Standard](https://portal.azure.com/#create/Microsoft.SQLServer2017StandardonSLES12SP2), [Web](https://portal.azure.com/#create/Microsoft.SQLServer2017WebonSLES12SP2), [Express](https://portal.azure.com/#create/Microsoft.FreeSQLServerLicenseSQLServer2017ExpressonSLES12SP2), [Developer](https://portal.azure.com/#create/Microsoft.FreeSQLServerLicenseSQLServer2017DeveloperonSLES12SP2) |
| **SQL Server 2017** | Ubuntu 16.04 LTS |[Enterprise](https://portal.azure.com/#create/Microsoft.SQLServer2017EnterpriseonUbuntuServer1604LTS), [Standard](https://portal.azure.com/#create/Microsoft.SQLServer2017StandardonUbuntuServer1604LTS), [Web](https://portal.azure.com/#create/Microsoft.SQLServer2017WebonUbuntuServer1604LTS), [Express](https://portal.azure.com/#create/Microsoft.FreeSQLServerLicenseSQLServer2017ExpressonUbuntuServer1604LTS), [Developer](https://portal.azure.com/#create/Microsoft.FreeSQLServerLicenseSQLServer2017DeveloperonUbuntuServer1604LTS) |

> [!NOTE]
> To see the available SQL Server virtual machine images for Windows, see [Overview of SQL Server on Azure Virtual Machines (Windows)](../windows/sql-server-on-azure-vm-iaas-what-is-overview.md).

## <a id="packages"></a> Installed packages

When you configure SQL Server on Linux, you install the Database Engine package and then several optional packages depending on your requirements. The Linux virtual machine images for SQL Server automatically install most packages for you. The following table shows which packages are installed for each distribution.

| Distribution | [Database Engine](/sql/linux/sql-server-linux-setup) | [Tools](/sql/linux/sql-server-linux-setup-tools) | [SQL Server agent](/sql/linux/sql-server-linux-setup-sql-agent) | [Full-text search](/sql/linux/sql-server-linux-setup-full-text-search) | [SSIS](/sql/linux/sql-server-linux-setup-ssis) | [HA add-on](/sql/linux/sql-server-linux-business-continuity-dr) |
|---|---|---|---|---|---|---|
| RHEL | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="RHEL and database engine." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="RHEL and tools." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="RHEL and SQL Server agent." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="RHEL and full-text search." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="RHEL and SSIS." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="RHEL and HA add-on." border="false"::: |
| SLES | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="SLES and database engine." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="SLES and tools." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="SLES and SQL Server agent." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="SLES and full-text search." border="false"::: | :::image type="content" source="../../media/applies-to/no-icon.svg" alt-text="SLES and SSIS (not supported)." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="SLES and HA add-on." border="false"::: |
| Ubuntu | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="Ubuntu and database engine." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="Ubuntu and tools." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="Ubuntu and SQL Server agent." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="Ubuntu and full-text search." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="Ubuntu and SSIS." border="false"::: | :::image type="content" source="../../media/applies-to/yes-icon.svg" alt-text="Ubuntu and HA add-on." border="false"::: |

> [!NOTE]
> SQL IaaS Agent extension for SQL Server on Azure Linux Virtual Machines is only available for Ubuntu Linux distribution.

## Related products and services

### Linux virtual machines

* [Azure Virtual Machines overview](/azure/virtual-machines/linux/overview)

### Storage

* [Introduction to Microsoft Azure Storage](/azure/storage/common/storage-introduction)

### Networking

* [Virtual Network overview](/azure/virtual-network/virtual-networks-overview)
* [IP addresses in Azure](/azure/virtual-network/ip-services/public-ip-addresses)
* [Create a Fully Qualified Domain Name in the Azure portal](/azure/virtual-machines/create-fqdn)

### SQL

* [SQL Server on Linux documentation](/sql/linux)
* [Azure SQL Database comparison](../../azure-sql-iaas-vs-paas-what-is-overview.md)

## Next steps

Get started with SQL Server on Linux virtual machines:

* [Create a SQL Server VM in the Azure portal](sql-vm-create-portal-quickstart.md)

Get answers to commonly asked questions about SQL Server VMs on Linux:

* [SQL Server on Azure Virtual Machines FAQ](frequently-asked-questions-faq.yml)
