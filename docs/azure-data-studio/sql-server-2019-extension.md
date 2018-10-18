---
title: Azure Data Studio SQL Server 2019 extension (preview) | Microsoft Docs
description: SQL Server 2019 Preview extension for Azure Data Studio
ms.custom: "tools|sos"
ms.date: "10/11/2018"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
manager: craigg
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# SQL Server 2019 extension (preview)

The SQL Server 2019 extension (preview) provides preview support for new features and tools shipping in support of [!INCLUDE[sql-server-2019](..\includes\sssqlv15-md.md)]. This includes preview support for [SQL Server 2019 big data clusters](../big-data-cluster/big-data-cluster-overview.md), an integrated [notebook experience](../big-data-cluster/notebooks-guidance.md), a PolyBase [Create External Table wizard](../relational-databases/polybase/data-virtualization.md?toc=%2fsql%2fbig-data-cluster%2ftoc.json), and [Azure Resource Explorer](azure-resource-explorer.md).

## Install the SQL Server 2019 extension (preview)

To install the SQL Server 2019 extension (preview), download and install the associated .vsix file.

1. Download the SQL Server 2019 extension (preview) .vsix file to a local directory:

   |Platform|Download|Release date|Version
   |:---|:---|:---|:---|
   |Windows|[.vsix](https://go.microsoft.com/fwlink/?LinkId=2031539)|October 17, 2018|0.7.2
   |macOS|[.vsix](https://go.microsoft.com/fwlink/?LinkId=2031717)|October 17, 2018 |0.7.2
   |Linux|[.vsix](https://go.microsoft.com/fwlink/?LinkId=2031538)|October 17, 2018 |0.7.2

1. In Azure Data Studio choose **Install Extension from VSIX Package** from the **File** menu and select the downloaded .vsix file.

1. Choose **Yes** when prompted to confirm installation and wait for the notification that the installation succeeded.

1. Select **Reload** to enable the extension (only required the first time you install an extension).

1. After reloading, the extension will install dependencies. You can see the progress in the Output window, and it could take several minutes.

## Release Notes (v0.7.2)
* Azure Resource Explorer is now built into Azure Data Studio and has been removed from this extension. Thank you for your feedback on this!
* Improved performance of notebooks with many Markdown cells.
* Auto-size code cells in Notebook. This still has a minimum size based on the cell toolbar.
* Notify user when installing Notebook dependencies. On Windows in particular this can take a long time, so notifications are now shown in the Tasks view.
* Support reinstalling Notebook dependencies. This is useful if the user previously closed Azure Data Studio partway through installation.
* Support canceling cell execution in Notebook.
* Improved reliability when using Create External Data wizard, specifically when connection errors occur.
* Block use of Create External Data wizard if Polybase is not enabled or running in the target server.
* Spelling and naming fixes related to SQL Server 2019 and Create External Data.
* Removed a large number of errors from the Azure Data Studio debug console.

##  SQL Server 2019 Big Data Cluster support

* Click **Add Connection** in *Object Explorer* and choose **SQL Server big data cluster** as the connection type.

   > [!TIP]
   > If you do not see the **SQL Server big data cluster** connection type, restart Azure Data Studio.

* Enter the host name or IP address of the cluster endpoint plus the username & password used to connect.
* Optionally, include a friendly display name in the **Name** field.
* Click **Connect** and you can then launch common tasks from the Dashboard, browse **HDFS** in Object Explorer, and run in-context tasks from there.
* To submit a Spark job against the cluster, right-click on the server node in *Object Explorer* and choose **Submit Spark Job** to bring up the submission dialog.
* To open a Notebook, see the next section.

For details, see [Big Data Clusters](../big-data-cluster/big-data-cluster-overview.md).


## Azure Data Studio Notebooks

* Open a notebook in one of the following ways:
  * Open a new notebook from the *Command Palette*.
  * Open the HDFS Object Explorer tree for a SQL Server 2019 big data cluster and either:
    * Right click on the server node and choose **New Jupyter Notebook**.
    * Right click on a CSV file, and choose **Analyze in Notebook**.
  * Open an existing .ipynb file from the **File** menu or file explorer *(.ipynb files must be upgraded to version 4 or higher to load properly)*
* Choose a kernel. For local notebook execution, choose Python 3. For remote execution, choose PySpark or Spark | Scala.
* Choose a SQL Server big data cluster endpoint to connect to if executing remotely (this is not necessary for local development with Python 3).
* Add code or markdown cells via the buttons in the notebook header. Remove cells with the trash can icon to the left of each cell.
* Run cells with the play button for code cells, and toggle markdown editing and preview with the eye icon

## Polybase Create External Table Wizard

* From a SQL Server 2019 instance the *Create External Table Wizard* can be opened in three ways:
  * Right click on a server, choose **Manage**, click on the tab for SQL Server 2019 (Preview), and choose **Create External Table**.
  * With a SQL Server 2019 instance selected in *Object Explorer*, bring up *Create External Wizard* via the *Command Palette*.
  * Right click on a SQL Server 2019 database in *Object Explorer* and choose **Create External Table**.
* In this version of the extension, external tables may be created to access remote SQL Server and Oracle tables.

  > [!NOTE]
  > While the External Table functionality is a SQL 2019 feature, the remote SQL Server may be running an earlier version of SQL Server.

* Choose whether you are accessing SQL Server or Oracle on the first page of the wizard and continue.
* You will be prompted to create a Database Master Key if one has not already been created (passwords of insufficient complexity will be blocked).
* Create a data source connection and named credential for the remote server.
* Choose which objects to map to your new external table.
* Choose **Generate Script** or **Create** to finish the wizard.
* After creation of the external table, it immediately appears in the object tree of the database where it was created.


## Known Issues

* If password is not saved when creating a connection, some actions such as submitting Spark Job may not succeed.
* Existing .ipynb notebooks must be upgraded to version 4 or higher to load contents in the viewer.
