---
title: SQL Server 2019 extension (preview)
titleSuffix: Azure Data Studio
description: SQL Server 2019 Preview extension for Azure Data Studio
ms.custom: "seodec18"
ms.date: "04/19/2019"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "yualan"
ms.author: "alayu"
manager: craigg
---
# SQL Server 2019 extension (preview)

The SQL Server 2019 extension (preview) provides preview support for new features and tools shipping in support of [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]. This includes preview support for [SQL Server 2019 big data clusters](../big-data-cluster/big-data-cluster-overview.md), an integrated [notebook experience](../big-data-cluster/notebooks-guidance.md), and a PolyBase [Create External Table wizard](../relational-databases/polybase/data-virtualization.md?toc=/sql/toc/toc.json).

## Install the SQL Server 2019 extension (preview)

To install the SQL Server 2019 extension (preview), download and install the associated .vsix file.

1. Download the SQL Server 2019 extension (preview) .vsix file to a local directory:

   |Platform|Download|Release date|Version
   |:---|:---|:---|:---|
   |Windows|[.vsix](https://go.microsoft.com/fwlink/?linkid=2087443)|April 18, 2019 |0.12.1
   |macOS|[.vsix](https://go.microsoft.com/fwlink/?linkid=2087442)|April 18, 2019 |0.12.1
   |Linux|[.vsix](https://go.microsoft.com/fwlink/?linkid=2087441)|April 18, 2019 |0.12.1

1. In Azure Data Studio choose **Install Extension from VSIX Package** from the **File** menu and select the downloaded .vsix file.

1. Choose **Yes** when prompted to confirm installation and wait for the notification that the installation succeeded.

1. Select **Reload** to enable the extension (only required the first time you install an extension).

1. After reloading, the extension will install dependencies. You can see the progress in the Output window, and it could take several minutes.

1. After the dependencies finish installing, close and reopen Azure Data Studio. The **SQL Server big data cluster** connection type is not available until you restart Azure Data Studio.

## Changes in release 0.12.1

* The **SQL Server big data cluster** connection type has been removed in this release. All functionality previously available from the SQL Server big data cluster connection is now available in the SQL Server connection.
* HDFS browsing can be found under the **Data Services** folder
* For notebooks the the PySpark and other big data kernels work when connected to the SQL Server master instance in your SQL Server big data cluster.
* Create External Table wizard:
  * Support for creating External Table using existing External Data Source.
  * Performance improvements across the wizard.
  * Improved handling of object names with special characters. In some cases these caused the wizard to fail
  * Reliability improvements for the Object Mapping page.
  * Removed system databases - 'DWConfiguration', 'DWDiagnostics', 'DWQueue' - from the databases dropdown.
  * Support for setting the External File Format object's name in the **Create External Table from CSV Files** wizard.
  * Added a refresh button to the first page of the **Create External Table from CSV Files** wizard.

## Release Notes (v0.11.0)

* Jupyter Notebook support, specifically support for the Python3 and Spark kernels, has been moved into Azure Data Studio. This extension is no longer required in order to use Notebooks.
* Multiple bug fixes in the External Data wizards:
  * Oracle type mappings have been updated to match changes shipped in SQL Server 2019 CTP 2.3.
  * Fixed an issue where new schemas typed into the table mapping controls were being lost.
  * Fixed an issue where checking a Database node in the table mappings did not result in all tables and views being checked.


## Release Notes (v0.10.2)
### SQL Server 2019 support
Support for SQL Server 2019 has been updated. On connecting to a SQL Server Big Data Cluster instance a new _Data Services_ folder will appear in the explorer tree. This has launch points for actions such as opening a new Notebook against the connection, submitting Spark jobs, and working with HDFS. Note that for some actions such as _Create External Data_ over a HDFS file/folder, the _SQL Server 2019 Preview_ extension must be installed.

### Notebook support
We have made significant updates to the Notebook user interface in this release. Our focus was on making it easy to read Notebooks that are shared with you. This meant removing all outline boxes around cells unless selected or hovered, adding hover support for easy cell-level actions without need to select a cell, and clarifying execution state by adding execution count, an animated _stop running_ button and more. We also added keyboard shortcuts for _New Notebook_ (`Ctrl+Shift+N`), _Run Cell_ (`F5`), _New Code Cell_ (`Ctrl+Shift+C`), _New Text Cell_ (`Ctrl+Shift+T`). Moving forward we will aim to have all key actions launchable by shortcut so let us know what you're missing!

Other improvements and fixes include:
* The _SQL Server 2019 Preview_ extension now prompts uses to pick an install directory for Python dependencies. It also no longer includes Python in the `.vsix file`, reducing overall extension size. The Python dependencies are needed to support Spark and Python3 kernels, so installing this extension is required to use these.
* Support for launching a new notebook from the command-line has been added. Launch with the arguments `--command=notebook.command.new --server=myservername` should open a new notebook and connect to this server.
* Performance fixes for notebooks with a large code length in cells. If code cells are over 250 lines they will have a scrollbar added.
* Improved .ipynb file support. Version 3 or higher is now supported. Please note that on saving files will be updated to version 4 or higher.
* The `notebook.enabled` user setting has been removed now that the built in Notebook viewer is stable
* High Contrast theme is now supported with a number of fixes to object layout in this case.
* Fixed #3680 where outputs sometimes showed a number of `,,,` characters incorrectly
* Fixed #3602 Editor disappears for cells after navigating away from azure data studio
* Support has been added to use Grid views for the `application/vnd.dataresource+json` output MIME type. This means many Notebooks that use this (for example by setting `pd.options.display.html.table_schema` in a Python notebook) will have nicer tabular outputs
Fixed #3959 Azure Data Studio tries to shutdown notebook server twice after closing notebook

#### Known issues
* On opening a Notebook the install python dialog will appear. Canceling this install will result in the Kernels and Attach To dropdowns not showing expected values. The workaround is to complete the Python installation.
* When a notebook is opened with a kernel that is not supported, the kernels and _attach to_ dropdowns will cause Azure Data Studio to hang. You will need to close Azure Data Studio and ensure you use a kernel that is supported (Python3, Spark | R, Spark | Scala, PySpark, PySpark3)
* Spark UI link fails when using PySpark3 or other Spark kernels against the SQL Server endpoint. As a workaround please click on Spark UI from the Dashboard, or connect using the SQL Server big data cluster connection type as this will have the correct Spark UI hyperlink

### Extensibility improvements
A number of improvements that help extenders were added in this release
* A new `ObjectExplorerNodeProvider` API allows extensions to contribute folders under SQL Server or other Connection nodes. This is how the `Data Services` node is added under SQL Server 2019 instances but could be used to add Monitoring or other folders easily to the UI.
* Two new context key values are available to help show/hide contributions to the dashboard.
  * `mssql:iscluster` indicates if this is a SQL Server 2019 Big Data Cluster
  * `mssql:servermajorversion` has the server version (15 for SQL Server 2019, 14 for SQL Server 2017 and so on). This can help if features should only be shown for SQL Server 2017 or greater, for example.


## Release Notes (v0.8.0)
*Notebooks*:
* Adding cells before / after existing cells is now supported by clicking the "More Actions" cell button
* **Add New Connection** option has been added to the connections in the "Attach To" dropdown
* A **Reinstall Notebook Dependencies** command has been added to assist with Python package updates, and solve cases where install was halted partway through by closing the application. This can be run from the command palette (use `Ctrl/Cmd+Shift+P` and type `Reinstall Notebook Dependencies`)
* The PROSE python package has been updated to 1.1.0 and includes a number of bug fixes. Use the **Reinstall Notebook Dependencies** command to update this package
* A **Clear Output** command is now supported by clicking the **More Actions** cell button
* Fixed the following customer reported issues:
  * Notebook session could not start on Windows due to PATH issues
  * Notebook could not be started from the root folder of a drive, such as C:\ or D:\
  * [#2820](https://github.com/Microsoft/azuredatastudio/issues/2820) Unable to edit notebooks created from ADS in VS Code
  * Spark UI link now works when running a Spark kernel
  * Renamed "Managed Packages" to "Install Packages"

*Create External Data*:

* Error messages are copyable and have been separated into a summary and detailed view for easier
* Improved UI layout and significantly improved reliability and error handling
* Fixed the following customer reported issues:
  * Tables with invalid column mappings are shown as disabled and a warning explains the error

## Release Notes (v0.7.2)
* Azure Resource Explorer is now built into Azure Data Studio and has been removed from this extension. Thank you for your feedback on this!
* Improved performance of notebooks with many Markdown cells.
* Auto-size code cells in Notebook. This still has a minimum size based on the cell toolbar.
* Notify user when installing Notebook dependencies. On Windows in particular this can take a long time, so notifications are now shown in the Tasks view.
* Support reinstalling Notebook dependencies. This is useful if the user previously closed Azure Data Studio partway through installation.
* Support canceling cell execution in Notebook.
* Improved reliability when using Create External Data wizard, specifically when connection errors occur.
* Block use of Create External Data wizard if PolyBase is not enabled or running in the target server.
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

## PolyBase Create External Table Wizard

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
* Running the **Reinstall Notebook Dependencies** command may show 2 tasks in the tasks view, one of which fails. This does not cause installation to fail
* Choosing **Add New Connection** in a Notebook and clicking cancel will cause **Select Connection** to be shown, even if you were already connected.
