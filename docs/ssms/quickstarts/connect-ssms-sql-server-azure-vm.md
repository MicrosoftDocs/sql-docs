---
title: Connect to a SQL Server instance on an Azure VM with SQL Server Management Studio (SSMS)
description: Connect to to a SQL Server instance on an Azure VM using SQL Server Management Studio (SSMS).
ms.prod: sql
ms.technology: ssms
ms.topic: quickstart
author: markingmyname
ms.author: maghan
ms.reviewer: sstein, mikeray
ms.custom: ""
ms.date: 12/14/2020
---

# Quickstart: Connect to a SQL Server instance on an Azure VM using SQL Server Management Studio (SSMS)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This quickstart explains how to use SQL Server Management Studio (SSMS) to connect to different data sources.

## Prerequisites

To complete this article, you need SQL Server Management Studio and access to a data source.

- Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).
- [SQL Server on an Azure VM](https://azure.microsoft.com/services/virtual-machines/sql-server/?&ef_id=CjwKCAiA17P9BRB2EiwAMvwNyP3g3mY45X8dbqEtIr8HASwSLUYRBrwwciZptYt0vUU4K7TuWnXTbxoCoPQQAvD_BwE:G:s&OCID=AID2100131_SEM_CjwKCAiA17P9BRB2EiwAMvwNyP3g3mY45X8dbqEtIr8HASwSLUYRBrwwciZptYt0vUU4K7TuWnXTbxoCoPQQAvD_BwE:G:s&gclid=CjwKCAiA17P9BRB2EiwAMvwNyP3g3mY45X8dbqEtIr8HASwSLUYRBrwwciZptYt0vUU4K7TuWnXTbxoCoPQQAvD_BwE).

## Connect to SQL Virtual Machines

The following steps show how to create an optional DNS label for your Azure VM and then connect with SQL Server Management Studio (SSMS).

### Configure a DNS Label for the public IP address

To connect to the SQL Server Database Engine from the Internet, consider creating a DNS Label for your public IP address. You can join by IP address, but the DNS Label creates an A Record that is easier to identify and abstracts the underlying public IP address.

> [!NOTE]
> DNS Labels are not required if you plan to only connect to the SQL Server instance within the same Virtual Network or only locally.

1. Create a DNS Label by selecting **Virtual machines** in the portal. Select your SQL Server VM to bring up its properties.

2. In the virtual machine overview, select your **Public IP address**.

    :::image type="content" source="media/connect-data-source-ssms/azure-sql-vm-ip-address-.png" alt-text="public ip address":::

3. In the properties for your Public IP address, expand **Configuration**.

4. Enter a DNS Label name. This name is an A Record that can be used to directly connect to your SQL Server VM by name instead of by IP Address.

5. Select the **Save** button.

    :::image type="content" source="media/connect-data-source-ssms/azure-sql-vm-dns-label.png" alt-text="dns label":::

### Connect

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

    :::image type="content" source="media/connect-data-source-ssms/connect-object-explorer.png" alt-text="Connect link in Object Explorer":::

2. The **Connect to Server** dialog box appears. Enter the following information:

    |   Setting   |   Suggested Value(s)   |   Description   |
    |--------------|-----------------------|-----------------|
    | **Server type** | Database engine | For **Server type**, select **Database Engine** (usually the default option). |
    | **Server name** | The fully qualified server name | For **Server name**, enter the name of your Azure SQL VM name. You can also use the Azure SQL VM IP address to connect. | 
    | **Authentication** | SQL Server Authentication | Use **SQL Server Authentication** for Azure SQL VM to connect. Also, if you have an Azure Active Directory environment setup, you can use any of the Azure Active Directory options. </br> </br> The **Windows Authentication** method isn't supported for Azure SQL VM. For more information, see [Azure SQL authentication](/azure/sql-database/sql-database-security-overview#access-management).|
    | **Username** | Server account user ID | The user ID from the server account used to create the server. A login is required when using **SQL Server Authentication**. |
    | **Password** | Server account password | The password from the server account used to create the server. A password is required when using **SQL Server Authentication**. |

    :::image type="content" source="media/connect-data-source-ssms/connect-to-azure-sql-vm-object-explorer.png" alt-text="Server name field for SQL virtual Machines":::

    You can also modify additional connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the options.

3. After you've completed all the fields, select **Connect**.

4. To verify that your SQL Server on Azure VM succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

    :::image type="content" source="media/connect-data-source-ssms/connect-azure-sql-vm.png" alt-text="Azure sql vm connection":::

## Troubleshoot connectivity issues

Although the portal provides options to configure connectivity automatically, it's useful to know how to manually configure connectivity. Understanding the requirements can also aid troubleshooting.

The following table lists the requirements to connect to SQL Server on Azure VM.

| Requirement | Description |
|---|---|
| [Enable SQL Server authentication mode](/sql/database-engine/configure-windows/change-server-authentication-mode#use-ssms) | SQL Server authentication is needed to connect to the VM remotely unless you have configured Active Directory on a virtual network. |
| [Create a SQL login](/sql/relational-databases/security/authentication-access/create-a-login) | If you're using SQL authentication, you need a SQL login with a user name and password that also has permissions to your target database. |
| Enable TCP/IP protocol | SQL Server must allow connections over TCP. |
| [Enable firewall rule for the SQL Server port](/sql/database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access) | The firewall on the VM must allow inbound traffic on the SQL Server port (default 1433). |
| [Create a network security group rule for TCP 1433](https://docs.microsoft.com/azure/virtual-network/manage-network-security-group#create-a-security-rule) | You must allow the VM to receive traffic on the SQL Server port (default 1433) if you want to connect over the internet. This isn't required for local and virtual-network-only connections. This step is only required in the Azure portal. |

> [!TIP]
> The steps in the preceding table are done for you when you configure connectivity in the portal. Use these steps only to confirm your configuration or to set up connectivity manually for SQL Server.

## Next steps

> [!div class="nextstepaction"]
> [Create and query a SQL Server database on an Azure VM](query-ssms-sql-server-azure-vm.md)