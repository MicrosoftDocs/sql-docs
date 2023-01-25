---
title: Data Virtualization Extension
description: Data Virtualization extension for Azure Data Studio
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.date: 11/04/2019
ms.service: azure-data-studio
ms.topic: conceptual
---

# Data Virtualization extension for Azure Data Studio

The Data Virtualization extension for Azure Data Studio provides support for the [External Table Wizard with ODBC data sources](../../relational-databases/polybase/data-virtualization.md).

## Install the Data Virtualization extension

To install the Data Virtualization extension, visit [Extend the functionality of Azure Data Studio](./add-extensions.md).

## Changes in release 1.0

* Extension renamed to Data Virtualization.
* Create External Table wizard:
  * Included guided notebooks for virtualization MongoDB and Teradata sources.
  * Added dialog to fill out variables in MongoDB and Teradata virtualization notebooks.

## Changes in release 0.16

* Create External Table wizard:
  * Improved error handling when loading tables and views on object-mapping page.

## Changes in release 0.15

* Create External Table wizard:
  * Reduced time taken to load table and column information on the object-mapping page.
  * Fixed a bug with loading existing database scoped credentials on the connection details page.
* Create External Table from CSV Files Wizard:
  * Increased default sample size used for PROSE parsing.

## Changes in release 0.14.1

* Support for CTP 3.1 data source support

## Changes in release 0.12.1

* The **SQL Server big data cluster** connection type has been removed in this release. All functionality previously available from the SQL Server big data cluster connection is now available in the SQL Server connection.
* HDFS browsing can be found under the **Data Services** folder
* For notebooks, the PySpark and other big data kernels work when connected to the SQL Server master instance in your SQL Server big data cluster.
* Create External Table wizard:
  * Support for creating External Table using existing External Data Source.
  * Performance improvements across the wizard.
  * Improved handling of object names with special characters. In some cases, these caused the wizard to fail
  * Reliability improvements for the Object-Mapping page.
  * Removed system databases - `DWConfiguration`, `DWDiagnostics`, `DWQueue` - from the databases dropdown.
  * Support for setting the External File Format object's name in the **Create External Table from CSV Files** wizard.
  * Added a refresh button to the first page of the **Create External Table from CSV Files** wizard.

## Release Notes (v0.11.0)

* Jupyter Notebook support, specifically support for the Python3 and Spark kernels, has been moved into Azure Data Studio. This extension is no longer required in order to use Notebooks.
* Multiple bug fixes in the External Data wizards:
  * Oracle type mappings have been updated to match changes shipped in SQL Server 2019 CTP 2.3.
  * Fixed an issue where new schemas typed into the table-mapping controls were being lost.
  * Fixed an issue where checking a Database node in the table-mappings didn't result in all tables and views being checked.

## Release Notes (v0.10.2)

### SQL Server 2019 support

Support for SQL Server 2019 has been updated. After connecting to a SQL Server Big Data Cluster instance, a new _Data Services_ folder appears in the explorer tree. The folder has launch points for actions such as opening a new notebook against the connection, submitting Spark jobs, and working with HDFS. Some actions, such as _Create External Data_ over an HDFS file/folder, the _SQL Server 2019_ extension must be installed.

### Notebook support

We have made significant updates to the notebook user interface. Our focus is on making it easy to read notebooks that are shared with you. This meant removing all outline boxes around cells unless selected or hovered, adding hover support for easy cell-level actions without need to select a cell, and clarifying execution state by adding execution count, an animated _stop running_ button and more. We also added keyboard shortcuts for _New Notebook_ (`Ctrl+Shift+N`), _Run Cell_ (`F5`), _New Code Cell_ (`Ctrl+Shift+C`), _New Text Cell_ (`Ctrl+Shift+T`). We aim to have all key actions launchable by shortcut so let us know what you're missing!

Other improvements and fixes include:

* The _SQL Server 2019_ extension now prompts users to pick an install directory for Python dependencies. It also no longer includes Python in the `.vsix file`, reducing overall extension size. The Python dependencies support Spark and Python3 kernels.
* Support for launching a new notebook from the command line has been added. Launch with the arguments `--command=notebook.command.new --server=myservername` should open a new notebook and connect to this server.
* Performance fixes for notebooks with a large code length in cells. If code cells are over 250 lines, a scrollbar is added.
* Improved `.ipynb` file support. Version 3 or higher is now supported. 

    >[!Note]
    > Saving files updates to version 4 or higher.

* The `notebook.enabled` user setting has been removed now that the built-in Notebook viewer is stable.
* High Contrast theme is now supported with a number of fixes to object layout in this case.
* Fixed #3680 where outputs sometimes showed a number of `,,,` characters incorrectly.
* Fixed #3602 Editor disappears for cells after navigating away from Azure Data Studio.
* Support has been added to use Grid views for the `application/vnd.dataresource+json` output MIME type. This means many notebooks that use this (for example by setting `pd.options.display.html.table_schema` in a Python notebook) have nicer tabular outputs.

#### Known issues

* When opening a Notebook, the install python dialog appears. Canceling this install results in the Kernels and Attach To dropdowns not showing expected values. The workaround is to complete the Python installation.
* When a notebook is opened with a kernel that isn't supported, the kernels and _attach to_ dropdowns causes Azure Data Studio to stop responding. Close Azure Data Studio and ensure you use a kernel that is supported (Python3, Spark | R, Spark | Scala, PySpark, PySpark3).
* Spark UI link fails when using PySpark3 or other Spark kernels against the SQL Server endpoint. As a workaround, select Spark UI from the Dashboard, or connect using the SQL Server big data cluster connection type as this has the correct Spark UI hyperlink.

### Extensibility improvements

A number of improvements that help extenders were added in this release.

* A new `ObjectExplorerNodeProvider` API allows extensions to contribute folders under SQL Server or other Connection nodes. This is how the `Data Services` node is added under SQL Server 2019 instances but could be used to add Monitoring or other folders easily to the UI.
* Two new context key values are available to help show/hide contributions to the dashboard.
  * `mssql:iscluster` indicates if this is a SQL Server 2019 Big Data Cluster
  * `mssql:servermajorversion` has the server version (15 for SQL Server 2019, 14 for SQL Server 2017, and so on). This can help if features should only be shown for SQL Server 2017 or greater, for example.

## Release Notes (v0.8.0)

*Notebooks*:

* Adding cells before / after existing cells are now supported by selecting the "More Actions" cell button
* **Add New Connection** option has been added to the connections in the "Attach To" dropdown
* A **Reinstall Notebook Dependencies** command has been added to assist with Python package updates, and solve cases where install was halted partway through by closing the application. This can be run from the command palette (use `Ctrl/Cmd+Shift+P` and type `Reinstall Notebook Dependencies`)
* The PROSE python package has been updated to 1.1.0 and includes a number of bug fixes. Use the **Reinstall Notebook Dependencies** command to update this package
* A **Clear Output** command is now supported by selecting the **More Actions** cell button
* Fixed the following customer reported issues:
  * Notebook session couldn't start on Windows due to PATH issues
  * Notebook couldn't be started from the root folder of a drive, such as C:\ or D:\
  * [#2820](https://github.com/Microsoft/azuredatastudio/issues/2820) Unable to edit notebooks created from ADS in VS Code
  * Spark UI link now works when running a Spark kernel
  * Renamed "Managed Packages" to "Install Packages"

*Create External Data*:

* Error messages are copyable and have been separated into a summary and detailed view for easier
* Improved UI layout and improved reliability and error handling
* Fixed the following customer reported issues:
  * Tables with invalid column mappings are shown as disabled and a warning explains the error

## Release Notes (v0.7.2)

* Azure Resource Explorer is now built into Azure Data Studio and has been removed from this extension. Thank you for your feedback on this!
* Improved performance of notebooks with many Markdown cells.
* Autosize code cells in Notebook. This still has a minimum size based on the cell toolbar.
* Notify user when installing Notebook dependencies. On Windows in particular this can take a long time, so notifications are now shown in the Tasks view.
* Support reinstalling Notebook dependencies. This is useful if the user previously closed Azure Data Studio partway through installation.
* Support canceling cell execution in Notebook.
* Improved reliability when using Create External Data wizard, specifically when connection errors occur.
* Block use of Create External Data wizard if PolyBase isn't enabled or running in the target server.
* Spelling and naming fixes related to SQL Server 2019 and Create External Data.
* Removed a large number of errors from the Azure Data Studio debug console.