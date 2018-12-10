---
title: SQL Server 2019 extension (preview)
titleSuffix: Azure Data Studio
description: SQL Server 2019 Preview extension for Azure Data Studio
ms.custom: "seodec18"
ms.date: "12/13/2018"
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

The SQL Server 2019 extension (preview) provides preview support for new features and tools shipping in support of [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]. This includes preview support for [SQL Server 2019 big data clusters](../big-data-cluster/big-data-cluster-overview.md), an integrated [notebook experience](../big-data-cluster/notebooks-guidance.md), a PolyBase [Create External Table wizard](../relational-databases/polybase/data-virtualization.md?toc=%2fsql%2fbig-data-cluster%2ftoc.json), and [Azure Resource Explorer](azure-resource-explorer.md).

[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] is available [here](https://aka.ms/ss19).
[!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)] big data cluster preview is available [here](https://aka.ms/eapsignup).


## Install the SQL Server 2019 extension (preview)

To install the SQL Server 2019 extension (preview), download and install the associated .vsix file.

1. Download the SQL Server 2019 extension (preview) .vsix file to a local directory:

   |Platform|Download|Release date|Version
   |:---|:---|:---|:---|
   |Windows|[.vsix](https://go.microsoft.com/fwlink/?linkid=2038184)|December 12, 2018 |0.9.1
   |macOS|[.vsix](https://go.microsoft.com/fwlink/?linkid=2038178)|December 12, 2018 |0.9.1
   |Linux|[.vsix](https://go.microsoft.com/fwlink/?linkid=2038246)|December 12, 2018 |0.9.1

2. In Azure Data Studio choose **Install Extension from VSIX Package** from the **File** menu and select the downloaded .vsix file.
3. Choose **Yes** when prompted to confirm installation and wait for the notification that the installation succeeded.
4. Select **Reload** to enable the extension (only required the first time you install an extension).
5. After reloading, the extension will install dependencies. You can see the progress in the Output window, and it could take several minutes.
6. If you experience any notebook related issues upgrading from previous releases to this latest version, open the **Command Palette** and type "Reinstall notebook dependencies.". You might need to close existing python processes that are running and relaunch Azure Data Studio.


## Supported Features

- SQL Server 2019 big data cluster support
   - Connect to the SQL Server big data cluster endpoint shipped with [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)].
   - Browse HDFS, upload, save, delete files and launch useful actions such as Analyze in Notebook for CSV files.
   - Try the new Create External Table from CSV Files wizard in HDFS, which lets you create an external table in your SQL Server Master instance associated with the cluster. You can virtualize the data from the remote HDFS Data sources without moving the data.
   - Submit Spark Jobs and view job status from the dashboard.

- Azure Data Studio Notebooks
   - Create or open Notebooks using an integrated Notebook viewer. In this release the Notebook viewer supports connecting to local kernels and the SQL Server 2019 big data cluster only.
   - You can also add a new connection from the “Attach To” dropdown. This lets you easily submit Notebooks to different clusters.
   - You can now run local Python 3 jobs in a Notebook and submit jobs against a SQL Server 2019 big data cluster using Python, Scala and R. R support is new in this release.
   - Use the PROSE Code Accelerator libraries in your Notebook to learn file format and data types for fast data preparation.

- SQL Server Polybase Create External Table Wizard
   - Create an external table and its supporting metadata structures with an easy to use wizard. In this release, remote SQL Server and Oracle servers are supported.
   - Launch Create External Table from CSV Files wizard in HDFS, which lets you create an external table in your SQL Server Master instance associated with the cluster. You can virtualize the data from the remote HDFS Data sources without ever needing to now move the data.


##  SQL Server 2019 Big Data Cluster support

- Click the **Add Connection** button in **Object Explorer** and choose **Microsoft SQL Server** or **SQL Server big data cluster** as the connection type.
- If you want to do SQL Server related operations, please connect to the SQL Server Master Instance by choosing **Microsoft SQL Server** as the connection type.
- If you want to do SQL Server big data cluster related operations, please connect to **SQL Server big data cluster** as the connection type.
- Enter the host name or IP address of the cluster endpoint plus the username and password used to connect.
- Optionally, include a friendly display name in the **Name** field.
- Click **Connect** and you can then launch common tasks from the Dashboard, browse HDFS in **Object Explorer**, and run in-context tasks from there.
- To submit a Spark job against the cluster, right-click on the server node in the Object Explorer, then right-click and choose Submit Spark Job to bring up the submission dialog.
- View the **Spark History** to monitor all the Spark applications which are running against the big data cluster. You can launch this from the Manage Dashboard.
- View the **YARN History** to monitor all the YARN applications which are running against the big data cluster. You can launch this from the Manage Dashboard.
- To open a Notebook, read below in the Azure Data Studio Notebook section.

For details, see [Big Data Clusters](../big-data-cluster/big-data-cluster-overview.md).


## Azure Data Studio Notebooks

- Open a notebook in one of the following ways:
  - Open a new notebook from the *Command Palette*.
  - Open the HDFS Object Explorer tree for a SQL Server 2019 big data cluster and either:
    - Right click on the server node and choose **New Jupyter Notebook**.
    - Right click on a CSV file, and choose **Analyze in Notebook**.
  - Open an existing .ipynb file from the **File** menu or file explorer *(.ipynb files must be upgraded to version 4 or higher to load properly)*.
- Choose a kernel. For local notebook execution, choose **Python 3**. For remote execution, choose **PySpark** or **Spark | Scala**.
- Choose a SQL Server big data cluster endpoint to connect to if executing remotely (this is not necessary for local development with Python 3).
- Add code or markdown cells via the buttons in the notebook header.
- Run cells with the play button for code cells, double click edit on the cell to start editing the markdowns.
- If you do not like the line highlight renderer in the code and the markdown cells, go to the user settings (click F1 and open user settings) and search for "editor.renderLineHighlight": "line", set that to none and re-open the Notebook. This should remove the line highlight renderer.


## PolyBase Create External Table Wizard

- From a SQL Server 2019 instance the *Create External Table Wizard* can be opened in three ways:
  - Right click on a server, choose **Manage**, click on the tab for SQL Server 2019 (Preview), and choose **Create External Table**.
  - With a SQL Server 2019 instance selected in *Object Explorer*, bring up *Create External Wizard* via the *Command Palette*.
  - Right click on a SQL Server 2019 database in *Object Explorer* and choose **Create External Table**.
- In this version of the extension, external tables may be created to access remote SQL Server and Oracle tables.

  > [!NOTE]
  > While the External Table functionality is a SQL Server 2019 feature, the remote SQL Server may be running an earlier version of SQL Server.

- Choose whether you are accessing SQL Server or Oracle on the first page of the wizard and continue.
- You will be prompted to create a Database Master Key if one has not already been created (passwords of insufficient complexity will be blocked).
- Create a data source connection and named credential for the remote server.
- Choose which objects to map to your new external table.
- Choose **Generate Script** or **Create** to finish the wizard.
- After creation of the external table, it immediately appears in the object tree of the database where it was created.

## Polybase Create External Table From CSV Files Wizard

- From connecting to the SQL Server big data cluster end point, you might need to create an external table over the files in your HDFS and this can be done in two ways:
  - Browse to the .csv file in HDFS over which you would like to Create External Table From and right click on the file and then launch the Create External Table From CSV Files wizard, choose the active SQL Server connections from the drop down and this will fill in the connection details. You will need to pass in the credentials to the SQL Server Master Instance which is associated with this end point for the SQL Server big data cluster connection end point.
  - Browse to the folder in HDFS which has the files with the same file extension and same schema and now when you launch the Create External Table From CSV Files then it will create an external table over all the files in the folder.



## Release Notes (v0.9.1)

*Notebooks*:

The Notebook viewer UI has moved into Azure Data Studio core. This improved performance when loading and updating a Notebook. The `notebook.enabled` configuration setting is true by default and means the new "built in" Notebook viewer will be used. This extension continues to provide Jupyter notebook compatibility, including installation of Python dependencies and the ability to connect to SQL Server 2019 big data resources (HDFS, Spark).

*Other improvements and fixes include*:

- The PROSE python package has been updated to 1.2.1 and includes a number of bug fixes. Use the Reinstall Notebook Dependencies command to update this package.
- Cells have a simpler look with just one Run Cell button on the action bar, a real context menu with additional actions on the right of the cell, and double-click to edit support on Markdown cells.
- A Notebook API is in progress. This is included in the proposed API file for Azure Data Studio. As this feature is in active development we expect some APIs to change, but these can be used to perform basic operations on a Notebook - Open New/Existing Notebook, query contents, add cells.

*Create External Data*:

- A new wizard to create external data sources from HDFS files/folders in SQL Server 2019 big data clusters has been added.
- Support for mapping SQL Server and Oracle Views has been added to the existing Create External Data wizard.
- Performance and reliability fixes for the existing wizard were added.





## Release Notes (v0.8.0)

*Notebooks*:
- Adding cells before / after existing cells is now supported by clicking the "More Actions" cell button
- **Add New Connection** option has been added to the connections in the "Attach To" dropdown
- A **Reinstall Notebook Dependencies** command has been added to assist with Python package updates, and solve cases where install was halted partway through by closing the application. This can be run from the command palette (use `Ctrl/Cmd+Shift+P` and type `Reinstall Notebook Dependencies`)
- The PROSE python package has been updated to 1.1.0 and includes a number of bug fixes. Use the **Reinstall Notebook Dependencies** command to update this package
- A **Clear Output** command is now supported by clicking the **More Actions** cell button
- Fixed the following customer reported issues:
  - Notebook session could not start on Windows due to PATH issues
  - Notebook could not be started from the root folder of a drive, such as C:\ or D:\
  - [#2820](https://github.com/Microsoft/azuredatastudio/issues/2820) Unable to edit notebooks created from ADS in VS Code
  - Spark UI link now works when running a Spark kernel
  - Renamed "Managed Packages" to "Install Packages"

*Create External Data*:

- Error messages are copyable and have been separated into a summary and detailed view for easier
- Improved UI layout and significantly improved reliability and error handling
- Fixed the following customer reported issues:
  - Tables with invalid column mappings are shown as disabled and a warning explains the error

## Release Notes (v0.7.2)

- Azure Resource Explorer is now built into Azure Data Studio and has been removed from this extension. Thank you for your feedback on this!
- Improved performance of notebooks with many Markdown cells.
- Auto-size code cells in Notebook. This still has a minimum size based on the cell toolbar.
- Notify user when installing Notebook dependencies. On Windows in particular this can take a long time, so notifications are now shown in the Tasks view.
- Support reinstalling Notebook dependencies. This is useful if the user previously closed Azure Data Studio partway through installation.
- Support canceling cell execution in Notebook.
- Improved reliability when using Create External Data wizard, specifically when connection errors occur.
- Block use of Create External Data wizard if PolyBase is not enabled or running in the target server.
- Spelling and naming fixes related to SQL Server 2019 and Create External Data.
- Removed a large number of errors from the Azure Data Studio debug console.


## Known Issues

- If password is not saved when creating a connection, some actions such as submitting Spark Job may not succeed.
- Existing .ipynb notebooks must be upgraded to version 4 or higher to load contents in the viewer.
- You will not be able to preview .ipynb files from HDFS.
- You will not be able to preview files in HDFS which are over 30MB.
- All the files in the HDFS folder for Create External Table From CSV Files to work properly would need to have the same file extension (.csv) and conform to the same schema. If there are .csv files which are of different schema then the wizard will still open but you will not be able to create the external table.
