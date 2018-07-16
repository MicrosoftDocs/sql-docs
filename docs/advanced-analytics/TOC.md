# Overview

## [What is SQL Server Machine Learning Services?](what-is-sql-server-machine-learning.md)
### [In-Database analytics](r/sql-server-r-services.md)
### [Standalone server](r/r-server-standalone.md)
## [New features](what-s-new-in-sql-server-machine-learning-services.md)
## [Features by edition](r/differences-in-r-features-between-editions-of-sql-server.md)

# [Architecture](architecture-overview-machine-learning.md)
## [R](r/architecture-overview-sql-server-r.md)
### [R interoperability](r/r-interoperability-in-sql-server.md)
### [Components that support R integration](r/new-components-in-sql-server-to-support-r.md)
### [Security for R](r/security-overview-sql-server-r.md)
## [Python](python/architecture-overview-sql-server-python.md)
### [Python in Machine Learning Services](python/sql-server-python-services.md)
### [Python interoperability](python/python-interoperability.md)
### [Components to support Python](python/new-components-in-sql-server-to-support-python-integration.md)
### [Python security](python/security-overview-sql-server-python-services.md)
<!-- ### [How To Create a Resource Pool for Python](python/how-to-create-a-resource-pool-for-python.md)-->
<!-- ### [Extended Events for Python](python/extended-events-for-python.md)-->
<!-- ### [DMVs for Python](python/dmvs-for-python.md)-->
<!-- ### [Resource Governance for Python](python/resource-governance-for-python.md)-->

# Install 

## SQL Server 2017
### [In-Database analytics](install/sql-machine-learning-services-windows-install.md)
### [Standalone server](install/sql-machine-learning-standalone-windows-install.md)
## SQL Server 2016
### [R Services (In-Database)](install/sql-r-services-windows-install.md)
### [R Server (Standalone)](install/sql-r-standalone-windows-install.md)
## [Pre-trained models](r/install-pretrained-models-sql-server.md)
## [Command-prompt setup](install/sql-ml-component-commandline-install.md)
## [Offline setup (no internet)](install/sql-ml-component-install-without-internet-access.md)
## [Upgrade R and Python](r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md)
## [Set up R tools](r/set-up-a-data-science-client.md)
## [Set up Python tools](python/setup-python-client-tools-sql.md)

# Quickstarts

## R
### [Hello World in R and SQL](tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
### [Handle inputs and outputs](tutorials/rtsql-working-with-inputs-and-outputs.md)
### [Handle data types and objects](tutorials/rtsql-r-and-sql-data-types-and-data-objects.md)
### [Create a predictive model](tutorials/rtsql-create-a-predictive-model-r.md)
### [Predict and plot from model](tutorials/rtsql-predict-and-plot-from-model.md)

# [Tutorials](tutorials/machine-learning-services-tutorials.md)
## [R](tutorials/sql-server-r-tutorials.md)
### [Learn in-database analytics](tutorials/sqldev-in-database-r-for-sql-developers.md)
#### [1 - Get data and scripts](tutorials/sqldev-download-the-sample-data.md)
#### [2 - Set up the environment](r/sqldev-import-data-to-sql-server-using-powershell.md)
#### [3 - Visualize data](tutorials/sqldev-explore-and-visualize-the-data.md)
#### [4 - Create data features](tutorials/sqldev-create-data-features-using-t-sql.md)
#### [5 - Train and save to SQL](r/sqldev-train-and-save-a-model-using-t-sql.md)
#### [6 - Predict outcomes](tutorials/sqldev-operationalize-the-model.md)

### [Data science end-to-end walkthrough](tutorials/walkthrough-data-science-end-to-end-walkthrough.md)
#### [Prerequisites](tutorials/walkthrough-prerequisites-for-data-science-walkthroughs.md)
#### [Prepare data](tutorials/walkthrough-prepare-the-data.md)
#### [Explore data using SQL](tutorials/walkthrough-view-and-explore-the-data.md)
#### [Summarize data using R](tutorials/walkthrough-view-and-summarize-data-using-r.md)
#### [Create graphs and plots](tutorials/walkthrough-create-graphs-and-plots-using-r.md)
#### [Create data features](tutorials/walkthrough-create-data-features.md)
#### [Build and save the model](tutorials/walkthrough-build-and-save-the-model.md)
#### [Deploy and use the model](tutorials/walkthrough-deploy-and-use-the-model.md)

### [Deep dive with RevoScaleR](tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
#### [Prerequisites](tutorials/deepdive-work-with-sql-server-data-using-r.md)
#### [Create data objects using RxSqlServerData](tutorials/deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)
#### [Query and modify data](tutorials/deepdive-query-and-modify-the-sql-server-data.md)
#### [Define a compute context](tutorials/deepdive-define-and-use-compute-contexts.md)
#### [Create and run R scripts](tutorials/deepdive-create-and-run-r-scripts.md)
#### [Visualize data](tutorials/deepdive-visualize-sql-server-data-using-r.md)
#### [Create models](tutorials/deepdive-create-models.md)
#### [Score new data](tutorials/deepdive-score-new-data.md)
#### [Transform data](tutorials/deepdive-transform-data-using-r.md)
#### [Load data into memory using rxImport](tutorials/deepdive-load-data-into-memory-using-rximport.md)
#### [Create a table using rxDataStep](tutorials/deepdive-create-new-sql-server-table-using-rxdatastep.md)
#### [Perform chunking analysis using rxDataStep](tutorials/deepdive-perform-chunking-analysis-using-rxdatastep.md)
#### [Analyze data in local compute context](tutorials/deepdive-analyze-data-in-local-compute-context.md)
#### [Move data between SQL Server and XDF file](tutorials/deepdive-move-data-between-sql-server-and-xdf-file.md)
#### [Create a simple simulation](tutorials/deepdive-create-a-simple-simulation.md)

## [Python](tutorials/sql-server-python-tutorials.md)
### [Run Python using T-SQL](tutorials/run-python-using-t-sql.md)
#### [Wrap Python in a stored procedure](tutorials/wrap-python-in-tsql-stored-procedure.md)
#### [Train and score from a Python model in SQL Server](tutorials/train-score-using-python-in-tsql.md)
#### [Create a model using revoscalepy in a SQL Server compute context](tutorials/use-python-revoscalepy-to-create-model.md)
### [Learn in-database analytics](tutorials/sqldev-in-database-python-for-sql-developers.md)
#### [Get data and scripts](tutorials/sqldev-py1-download-the-sample-data.md)
#### [Import data](tutorials/sqldev-py2-import-data-to-sql-server-using-powershell.md)
#### [Explore and visualize data](tutorials/sqldev-py3-explore-and-visualize-the-data.md)
#### [Create data features](tutorials/sqldev-py4-create-data-features-using-t-sql.md)
#### [Train and save the model](tutorials/sqldev-py5-train-and-save-a-model-using-t-sql.md)
#### [Operationalize the model](tutorials/sqldev-py6-operationalize-the-model.md)

# [Samples](https://github.com/Microsoft/sql-server-samples)

# [Solutions](tutorials/data-science-scenarios-and-solution-templates.md)

# [How To](r/sql-server-machine-learning-tasks.md)

## Package management
### [Default packages](r/installing-and-managing-r-packages.md)
### [Get package information](r/determine-which-packages-are-installed-on-sql-server.md)
### [Install new Python packages](python/install-additional-python-packages-on-sql-server.md)
### [Install new R packages](r/install-additional-r-packages-on-sql-server.md)
#### [Use R package managers](r/use-r-package-managers-on-sql-server.md)
#### [Use T-SQL](r/install-r-packages-tsql.md)
#### [Use RevoScaleR](r/use-revoscaler-to-manage-r-packages.md)
##### [Enable remote R package management](r/r-package-how-to-enable-or-disable.md)
##### [Synchronize R packages](r/package-install-uninstall-and-sync.md)
#### [Create a miniCRAN repo](r/create-a-local-package-repository-using-minicran.md)
#### [Tips for using R packages](r/packages-installed-in-user-libraries.md)

## Data exploration and modeling
### [R libraries and data types](r/r-libraries-and-data-types.md)
### [Python libraries and data types](python/python-libraries-and-data-types.md)
### [Native scoring](sql-native-scoring.md)
### [Real-time scoring](real-time-scoring.md)
### [Predictive modeling with R](r/data-exploration-and-predictive-modeling-with-r.md)
### [How to perform real-time or native scoring](r/how-to-do-realtime-scoring.md)
### [Load R objects using ODBC](r/save-and-load-r-objects-from-sql-server-using-odbc.md)
### [Converting R Code for Use in Machine Learning Services](r/converting-r-code-for-use-in-sql-server.md)
### [Creating multiple models using rxExecBy](r/creating-multiple-models-using-rxexecby.md)
### [Use data from OLAP cubes in R](r/using-data-from-olap-cubes-in-r.md)
### [Create a stored procedure using sqlrutils](r/how-to-create-a-stored-procedure-using-sqlrutils.md)

## Performance
### [Performance tuning for R - Overview](r/sql-server-r-services-performance-tuning.md)
### [Performance tuning for R - SQL Server configuration)](r/sql-server-configuration-r-services.md)
### [Performance tuning for R - R and data optimization](r/r-and-data-optimization-r-services.md)
### [Performance tuning for R - Results](r/performance-case-study-r-services.md)
### [Use R code profiling functions](r/using-r-code-profiling-functions.md)

## Administration
### [Configure and manage R](r/configuration-sql-server-r-services.md)
### [Advanced configuration options for Machine Learning Services](r/configure-and-manage-advanced-analytics-extensions.md)
### [Security considerations for the R runtime in SQL Server](r/security-considerations-for-the-r-runtime-in-sql-server.md)
### [Modify the user account pool for SQL Server Machine Learning Services](r/modify-the-user-account-pool-for-sql-server-r-services.md)
### [Add SQLRUserGroup as a database user](r/add-sqlrusergroup-to-database.md)
### [Deploy and consume models using web services](operationalization-with-mrsdeploy.md)
### [Manage and monitor solutions](r/managing-and-monitoring-r-solutions.md)
### [Resource governance for Machine Learning Services](r/resource-governance-for-r-services.md)
### [Create a resource pool for machine learning](r/how-to-create-a-resource-pool-for-r.md)
### [Extended events for Machine Learning Services](r/extended-events-for-sql-server-r-services.md)
### [Extended events for monitoring PREDICT statements](xe-event-predict-tsql.md)
### [DMVs for Machine Learning Services](r/dmvs-for-sql-server-r-services.md)
### [Using R code profiling functions](r/using-r-code-profiling-functions.md)
### [Monitor Machine Learning Services using custom reports in Management Studio](r/monitor-r-services-using-custom-reports-in-management-studio.md)

# [Package reference](r/machine-learning-services-r-reference.md)

## [MicrosoftML in SQL](using-the-microsoftml-package.md)
## [RevoScaleR in SQL](r/revoscaler-overview.md)
## [RevoScaleR function list for SQL Server data](r/scaler-functions-for-working-with-sql-server-data.md)
## [sqlrutils in SQL](r/generating-an-r-stored-procedure-for-r-code-using-the-sqlrutils-package.md)
## [olapR in SQL](r/how-to-create-mdx-queries-using-olapr.md)
## [revoscalepy in SQL](python/what-is-revoscalepy.md)

# Resources

## [Known issues](known-issues-for-sql-server-machine-learning-services.md)
## [Release notes](https://docs.microsoft.com/sql/sql-server/sql-server-2017-release-notes)
## [Set up a virtual machine](r/installing-sql-server-r-services-on-an-azure-virtual-machine.md)
## [Troubleshooting](machine-learning-troubleshooting-faq.md)
### [Data collection](data-collection-ml-troubleshooting-process.md)
### [Install and upgrade errors](r/upgrade-and-installation-faq-sql-server-r-services.md)
### [Launchpad and external script execution errors](common-issues-external-script-execution.md)
### [R scripting errors](r-script-execution-errors.md)

## Blogs
### [SQL Server](https://blogs.technet.microsoft.com/dataplatforminsider/)
### [R Server](https://blogs.msdn.microsoft.com/microsoftrservertigerteam/)
### [Machine Learning](https://blogs.technet.microsoft.com/machinelearning/)

## Forums
### [SQL Server](https://social.msdn.microsoft.com/Forums/sqlserver/home?category=sqlserver)
### [Machine Learning Server](https://social.msdn.microsoft.com/Forums/home?forum=MicrosoftR)
