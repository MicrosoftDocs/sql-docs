---
title: "How to Work with CDC Services"
description: "How to Work with CDC Services"
author: chugugrace
ms.author: chugu
ms.reviewer: mikeray
ms.date: 02/24/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
---
# How to Work with CDC Services

[!INCLUDE [oracle-cdc-retirement](../includes/attunity-oracle-cdc-retirement.md)]

This procedure describes how to use the CDC Service Configuration Console to prepare a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance to work with Oracle CDC Services and to create a new CDC service.

<a id="to-work-with-cdc-services"></a>

### Work with CDC services

1. From the **Start** menu, select the **CDC Service Configuration for Oracle**.

1. From the left pane, select **Local CDC Services** (the root level).

1. You carry out the one or both of the following tasks:

   - **Prepare SQL Server**

     Select this option from the **Actions** pane on the right side of the CDC Service Configuration Console.

     You can also right-click **Local CDC Services** and select **Prepare SQL Server**.

     The Preparing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Instance for Oracle CDC dialog box opens.

     To prepare the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for Oracle CDC services, the login must have a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login with the **dbcreator** fixed server role. This login is used to create the MSXDBCDC database that is required for adding Oracle CDC Services and, later on, Oracle CDC Instances.

     For information on how to use this dialog box, see [Prepare SQL Server for CDC](the-oracle-cdc-service.md#prepare-sql-server-for-cdc). For information on how to enable a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance for CDC, see [How to Prepare SQL Server for CDC](how-to-prepare-sql-server-for-cdc.md).

   - **Create a new CDC service**

     Select **New Service** from the **Actions** pane on the right side of the CDC Service Configuration Console.

     You can also right-Click **Local CDC Services** and select **New Service**.

     The New Oracle CDC Service dialog box opens.

     For information on how to use this dialog box, see [Create and Edit an Oracle CDC Service](the-oracle-cdc-service.md#create-and-edit-an-oracle-cdc-service). For information on how to create or edit a CDC service, see [How to Create and Edit a CDC Service](how-to-create-and-edit-a-cdc-service.md).

     The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login used by the Oracle CDC Service only needs to be a member of the `public` fixed-server role, no other privileges are needed. However, to create the Oracle CDC Service, the login must have write permission to the MSXDBCDC database, for example the **db_owner** database role must be assigned to the login. When a login without write permission to the MSXDBDCDC database attempts to create a new Oracle CDC instance an error message is displayed. Select **OK** in that dialog box to display the Connect to SQL Server dialog box.

     For information on how to enter the credentials for a login that has write permission to the MSXDBCDC database, such the **db_owner** database role, see [Create and Edit an Oracle CDC Service](the-oracle-cdc-service.md#create-and-edit-an-oracle-cdc-service) and [Connection to SQL Server](the-oracle-cdc-service.md#connection-to-sql-server).

## Related content

- [Work with CDC Services](the-oracle-cdc-service.md#work-with-cdc-services)
