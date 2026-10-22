---
title: Using the dta Command Prompt Utility
description: Learn about the functionality that the dta command-prompt utility offers in addition to that provided by the SQL Server Database Engine Tuning Advisor.
author: thesqlsith
ms.author: derekw
ms.date: 01/02/2026
ms.service: sql
ms.subservice: performance
ms.topic: how-to
ms.collection:
  - data-tools
ms.custom:
  - sfi-image-nochange
helpviewer_keywords:
  - "Database Engine [SQL Server], tutorials"
---
# Lesson 3: Using the dta Command Prompt Utility

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The **dta** command-line utility extends the functionality of the Database Engine Tuning Advisor by supporting XML-based input and advanced tuning scenarios. You can use standard XML tools to create input files based on the Database Engine Tuning Advisor XML schema, which is installed with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and is also available online.

- Local: `C:\Program Files (x86)\Microsoft SQL Server\110\Tools\Binn\schemas\sqlserver\2004\07\dta\dtaschema.xsd`
- Online: [Microsoft SQL Server XML Schemas](https://go.microsoft.com/fwlink/?linkid=43100&clcid=0x409)

The XML schema provides greater flexibility when defining tuning options, including support for "what-if" analysis. You can evaluate existing and hypothetical physical database designs without implementing them, which enables performance assessment with minimal overhead. Hypothetical designs can be iteratively modified and reanalyzed until the desired performance characteristics are achieved.

By using XML input files with the **dta** utility, you can automate Database Engine Tuning Advisor operations and integrate them into scripts or other database design workflows.

This lesson demonstrates how to start the **dta** utility from the command prompt, view its syntax help, and tune an existing workload using the file `MyScript.sql`, which you created in [Lesson 2: Using Database Engine Tuning Advisor](lesson-2-using-database-engine-tuning-advisor.md).

Detailed use of XML input files is outside the scope of this lesson.

[!INCLUDE [article-uses-adventureworks](../../includes/article-uses-adventureworks.md)]

## Prerequisites

To complete this tutorial, you need SQL Server Management Studio, access to a server that's running SQL Server, and the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] database.

- Install [SQL Server 2022 Developer Edition](https://www.microsoft.com/sql-server/sql-server-downloads).
- Download [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md).

For instructions on restoring databases in SSMS, see [Restore a Database Backup Using SSMS](../../relational-databases/backup-restore/restore-a-database-backup-using-ssms.md).

  > [!NOTE]  
  > This tutorial is meant for a user familiar with using SQL Server Management Studio and basic database administration tasks.

## Access DTA command prompt utility help menu

1. On the **Start** menu, point to **All Programs**, point to **Accessories**, and then select **Command Prompt**.

1. At the command prompt, type the following command, and press **Enter**:

   ```console
   dta -? | more
   ```

   The `| more` part of this command is optional. However, using it enables you to page through the syntax help for the utility. Press **Enter** to advance the help text by the line, or press **Space** to advance it by the page.

   :::image type="content" source="media/dta-tutorials/dta-cmd-help.png" alt-text="Screenshot of Using help with DTA cmd utility.":::

## Tune simple workload using the DTA command prompt utility

1. At the command prompt, go to the directory where you stored the `MyScript.sql` file.

1. At the command prompt, type the following command. Press **Enter** to run the command and start the tuning session. The utility is case-sensitive when it parses commands:

   ```console
   dta -S YourServerName\YourSQLServerInstanceName -E -D AdventureWorks2022 -if MyScript.sql -s MySession2 -of MySession2OutputScript.sql -ox MySession2Output.xml -fa IDX_IV -fp NONE -fk NONE
   ```

   In this example:

   - `-S` specifies the name of your server and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance where the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database is installed.

   - `-E` specifies that you want to use a trusted connection to the instance, which is appropriate if you're connecting with a Windows domain account.

   - `-D` specifies the database that you want to tune, `-if` specifies the workload file, `-s` specifies the session name, `-of` specifies the file to which you want the tool to write the [!INCLUDE [tsql](../../includes/tsql-md.md)] recommendations script, and `-ox` specifies the file to which you want the tool to write the recommendations in XML format.

   - The last three switches specify tuning options as follows: `-fa IDX_IV` specifies that Database Engine Tuning Advisor should only consider adding indexes (both clustered and nonclustered) and indexed views; `-fp NONE` specifies that no partition strategy should be considered during analysis; and `-fk NONE` specifies that no existing physical design structures in the database must be kept when Database Engine Tuning Advisor makes its recommendations.

   :::image type="content" source="media/dta-tutorials/dta-cmd.png" alt-text="Screenshot of using CMD with DTA." lightbox="media/dta-tutorials/dta-cmd.png":::

1. After Database Engine Tuning Advisor finishes tuning the workload, it displays a message indicating that the tuning session completed successfully. You can view the tuning results by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to open the files `MySession2OutputScript.sql` and `MySession2Output.xml`.

   Alternatively, you can also open the `MySession2` tuning session in the Database Engine Tuning Advisor GUI and view its recommendations and reports in the same way that you did in [Lesson 1: Basic navigation in Database Engine Tuning Advisor (DTA)](lesson-1-basic-navigation-in-database-engine-tuning-advisor.md) and [Lesson 2: Using Database Engine Tuning Advisor](lesson-2-using-database-engine-tuning-advisor.md).

## After you finish this tutorial

After you finish the lessons in this tutorial, see the following articles:

- [Database Engine Tuning Advisor](../../relational-databases/performance/database-engine-tuning-advisor.md) for descriptions of how to perform tasks with this tool.

- [dta Utility](dta-utility.md) for reference material on the command prompt utility and the optional XML file you can use to control the operation of the utility.

To return to the start of the tutorial, see [Tutorial: Database Engine Tuning Advisor](tutorial-database-engine-tuning-advisor.md).

## Related content

- [Database Engine Tutorials](../../relational-databases/database-engine-tutorials.md)
