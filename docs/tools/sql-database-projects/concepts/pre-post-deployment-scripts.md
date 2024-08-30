---
title: Pre- and post-deployment scripts
description: "Add custom scripts for pre/post-deployment execution."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: concept-article
zone_pivot_groups: sq1-sql-projects-tools
---

# Pre- and post-deployment scripts overview

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Pre- and post-deployment scripts are SQL scripts that are included in the project to be executed during deployment. Pre/post-deployment scripts are included in the `.dacpac` but they aren't compiled into or validated with database object model. A pre-deployment script is executed before the deployment plan is executed but the deployment plan is calculated before the script executes. A post-deployment script is executed after the deployment plan completes.

## SQL project file sample and syntax

A SQL project file can have a single pre-deployment script and a single post-deployment script specified.

The following example from a SQL project file adds the file `populate-app-settings.sql` as post-deployment script.

```xml
...
  <ItemGroup>
    <PostDeploy Include="populate-app-settings.sql" />
  </ItemGroup>
</Project>
```

Multiple files can be executed as part of a pre- or post-deployment script by using a SQLCMD script that calls each file in order.

```sql
:r .\scripts\script1.sql
:r .\scripts\script2.sql
```

Those files should be excluded from the database model build by setting the `Build Action` property to `None` in the file properties in Visual Studio or by adding an entry for the file in the `.sqlproj` file with the `Build` attribute set to `None`.

```xml
...
  <ItemGroup>
    <None Include="scripts\script1.sql" />
    <None Include="scripts\script2.sql" />
  </ItemGroup>
</Project>
```

## Add pre- and post-deployment scripts

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project and select **Add** > **Script**. Select **Pre-Deployment Script** or **Post-Deployment Script**.

The script file is added to the project and opened in the query editor, where you can complete the script. This script will be executed before or after the deployment plan is executed every time the project is deployed.

::: zone-end

<!-- ::: zone pivot="sq1-visual-studio-sdk"

::: zone-end -->

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project and select **Add Pre-Deployment Script** or **Add Post-Deployment Script**. Provide a script name without the file extension.

The script file is added to the project and opened in the query editor, where you can complete the script. This script will be executed before or after the deployment plan is executed every time the project is deployed.

::: zone-end

::: zone pivot="sq1-command-line"

Edit the `.sqlproj` file directly to add pre- or post-deployment scripts. Add a `<PreDeploy>` or `<PostDeploy>` item to the `<ItemGroup>` section of the `.sqlproj` file.

For example, to add the script `scripts\before-script.sql` to our project as a pre-deployment script:

```xml
...
  <ItemGroup>
    <PreDeploy Include="scripts\before-script.sql" />
  </ItemGroup>
```

This script `scripts\before-script.sql` is executed before the deployment plan is executed every time the project is deployed.

::: zone-end

## Related content

- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
- [SQLCMD variables overview](sqlcmd-variables.md)
- [Tutorial: Create and deploy a SQL project](../tutorials/creating-and-deploying-a-sql-project.md)
