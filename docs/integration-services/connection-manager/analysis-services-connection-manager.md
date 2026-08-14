---
title: Analysis Services Connection Manager
description: Use a SQL Server Analysis Services (SSAS) connection manager to connect packages between SQL Server to SSAS.
ms.date: 08/13/2026
ms.service: sql
ms.subservice: integration-services
ms.topic: concept-article
f1_keywords:
  - "sql13.dts.designer.olapconnection.f1"
helpviewer_keywords:
  - "connections [Integration Services], Analysis Services"
  - "connection managers [Integration Services], Analysis Services"
  - "Analysis Services connection manager"
---
# Analysis Services connection manager

[!INCLUDE [sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] (SSAS) connection manager enables a package to connect to a server that runs an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] database or to an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] project that provides access to cube and dimension data.

You can only connect to an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] project while developing packages in [!INCLUDE [ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. At run time, packages connect to the server and the database to which you deployed the [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] project.

Both tasks, such as the [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] Execute DDL task and the [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] Processing task, and destinations, such as the Data Mining Model Training destination, use an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager.

For more information about [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] databases, see [Multidimensional Model Databases (SSAS)](/analysis-services/multidimensional-models/multidimensional-model-databases-ssas).

## Configuration of the Analysis Services connection manager

When you add an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager to a package, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)] creates a connection manager that is resolved as an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] connection at run time, sets the connection manager properties, and adds the connection manager to the **Connections** collection on the package. The **ConnectionManagerType** property of the connection manager is set to `MSOLAP100`.

You can configure the [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] connection manager in the following ways:

- Provide a connection string configured to meet the requirements of the Microsoft OLE Provider for Analysis Services provider.

- Specify the instance of [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] or the [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] project to connect to.

- If you're connecting to an instance of [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], specify the authentication mode.

  > [!NOTE]  
  > If you use SSIS in Azure Data Factory and want to connect to an instance of Azure Analysis Services, you can't use an account with multi-factor authentication (MFA) enabled, but must use an account that doesn't require any interactivity/MFA or a service principal instead.
  >
  > To use the latter, see [Automation with service principals](/azure/analysis-services/analysis-services-service-principal) to create one and assign the server administrator role to it, then select **Use a specific user name and password** to sign in to the server in your connection manager, and finally enter `User name: app:<application-id>` and `Password: <authorization-key>`. (Replace `<application-id>` and `<authorization-key>` with valid values.)

- Indicate whether the connection that is created from the connection manager is retained at run time.

You can set properties through [!INCLUDE [ssIS](../../includes/ssis-md.md)] Designer or programmatically.

## Add Analysis Services Connection Manager dialog box UI reference

Use the **Add Analysis Services Connection Manager** dialog box to create a connection to a server running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], or to edit connection properties.

### Options

#### Create a connection to a computer running Analysis Services

Use the default connection to a server that is running an instance of [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], or create a new connection by selecting **Edit**.

#### Edit

Use the **Connection Manager** dialog box to create a connection to a server that is running an instance of [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], and to edit connection properties.

#### Create a connection to an Analysis Services project in this solution

Specify that the connection uses an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] project in the open solution.

> [!NOTE]  
> Analysis Services tabular model projects aren't supported for this scenario.

#### Analysis Services project

Select an [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] project from the list.

## Related content

- [Add Connections Programmatically](../building-packages-programmatically/adding-connections-programmatically.md)
