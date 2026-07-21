---
title: "Quickstart: Create a Local Copy of a Database in a Container Using sqlcmd"
description: A quickstart that walks through using creating a new container and restoring a database
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: dlevy, maghan
ms.date: 01/28/2026
ms.service: sql
ms.subservice: tools-other
ms.topic: quickstart
ms.collection:
  - data-tools
ms.custom:
  - linux-related-content
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017"
---

# Quickstart: Create a new local copy of a database in a container with sqlcmd

In this quickstart, you'll use a single command in **sqlcmd** to create a new container, and restore a database to that container to create a new local copy of a database, for development or testing.

## Prerequisites

- A container runtime installed, such as [Docker](https://www.docker.com/) or [Podman](https://podman.io/)
- Install the [MSSQL extension for Visual Studio Code](../visual-studio-code-extensions/mssql/mssql-extension-visual-studio-code.md)
- Install the latest **sqlcmd**

## Remarks

Installing **sqlcmd** (Go) via a package manager replaces **sqlcmd** (ODBC) with **sqlcmd** (Go) in your environment path. Any current command line sessions need to be closed and reopened for this change to take to effect. **sqlcmd** (ODBC) isn't removed, and can still be used by specifying the full path to the executable.

You can also update your `PATH` variable to indicate which version takes precedence. To do so in Windows 11, open **System settings** and go to **About > Advanced system settings**. When **System Properties** opens, select the **Environment Variables** button. In the lower half, under **System variables**, select **Path** and then select **Edit**. If the location **sqlcmd** (Go) is saved to (`C:\Program Files\sqlcmd` is default) is listed before `C:\Program Files\Microsoft SQL Server\<version>\Tools\Binn`, then **sqlcmd** (Go) is used.

You can reverse the order to make **sqlcmd** (ODBC) the default again.

## Download and install sqlcmd (Go)

For more information, see [Download and install the sqlcmd utility](sqlcmd-download-install.md#download-and-install-sqlcmd-go).

## What problem will we solve?

This quickstart walks through the process of creating a local copy of a database, then querying it to analyze spending by customer.

## Create a new container and restore a database

[!INCLUDE [sqlcmd-create-container](../../includes/paragraph-content/sqlcmd-create-container.md)]

## Query the database in Visual Studio Code

Now that you have a local copy of your database, you can run queries.

Connect to the database with the MSSQL extension for Visual Studio Code, and run the following query to analyze spending by customer:

```sql
SELECT bg.BuyingGroupName AS CustomerName,
        COUNT(DISTINCT i.InvoiceID) AS InvoiceCount,
        COUNT(il.InvoiceLineID) AS InvoiceLineCount,
        SUM(il.LineProfit) AS Profit,
        SUM(il.ExtendedPrice) AS ExtendedPrice
FROM Sales.Invoices AS i
    INNER JOIN Sales.Customers AS c
        ON i.CustomerID = c.CustomerID
    INNER JOIN Sales.InvoiceLines AS il
        ON i.InvoiceID = il.InvoiceID
    INNER JOIN Sales.BuyingGroups AS bg
        ON c.BuyingGroupID = bg.BuyingGroupID
GROUP BY bg.BuyingGroupName
UNION
SELECT c.CustomerName,
        COUNT(DISTINCT i.InvoiceID) AS InvoiceCount,
        COUNT(il.InvoiceLineID) AS InvoiceLineCount,
        SUM(il.LineProfit) AS Profit,
        SUM(il.ExtendedPrice) AS ExtendedPrice
FROM Sales.Invoices AS i
    INNER JOIN Sales.Customers AS c
        ON i.CustomerID = c.CustomerID
    INNER JOIN Sales.InvoiceLines AS il
        ON i.InvoiceID = il.InvoiceID
    LEFT OUTER JOIN Sales.BuyingGroups AS bg
        ON c.BuyingGroupID = bg.BuyingGroupID
WHERE bg.BuyingGroupID IS NULL
GROUP BY c.CustomerName
ORDER BY Profit DESC;
```

## How did we solve the problem?

You were able to quickly create a local copy of a database for development and testing purposes. With a single command, you created a new local instance and restored the most recent backup to it. You then ran another command to connect to it via Visual Studio Code. You then queried the database using the MSSQL extension for Visual Studio Code to analyze spending by customer.

## Clean up resources

When you're done trying out the database, delete the container with the following command:

```bash
sqlcmd delete --force
```

The `--force` flag is used here for convenience since we are in a demo environment. In most cases, it's better to leave the `--force` flag off to make sure you aren't inadvertently deleting a database you don't mean to.

## Related content

- [Create and query a SQL Server container](sqlcmd-use-utility.md#create-and-query-a-sql-server-container)
