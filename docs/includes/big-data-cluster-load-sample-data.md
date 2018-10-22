### <a id="sampledata"></a> Load sample data

SQL Server big data cluster tutorials use a common set of sample data. Use the following steps to configure the sample data on your big data cluster:

1. If you do not have the SQL Server command-line tools (**sqlcmd** and **bcp**) installed, first install these tools with one of the links:

   * **Windows**: [Install SQL Server command-line tools on Windows](https://www.microsoft.com/download/details.aspx?id=53591)
   * **Linux**: [Install SQL Server command-line tools on Linux](https://docs.microsoft.com/sql/linux/sql-server-linux-setup-tools)

1. Download the sample backup file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak) to your machine.

1. Navigate to the SQL Server 2019 big data cluster [samples directory](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster).

1. Download the [bootstrap-sample-db.sql](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.sql) Transact-SQL script.

1. Download and run one of the following two sample scripts from the command line:

   * **Windows**: [bootstrap-sample-db.cmd](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.cmd)
   * **Linux**: [bootstrap-sample-db.sh](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.sh)

   > [!TIP]
   > You can get usage instructions by running the script with no parameters.

The script restores the sample database to the SQL Server master instance and also loads data into HDFS in the storage pool.