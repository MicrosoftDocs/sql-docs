### <a id="sampledata"></a> Load sample data

SQL Server big data cluster tutorials use a common set of sample data. You can load this sample data into your cluster with the following steps:

1. Download the sample backup file [tpcxbb_1gb.bak](https://sqlchoice.blob.core.windows.net/sqlchoice/static/tpcxbb_1gb.bak) to your machine.
1. Navigate to the SQL Server 2019 big data cluster [samples directory](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/sql-big-data-cluster).
1. Download the [bootstrap-sample-db.sql](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.sql) Transact-SQL script.
1. Download and run one of the following two sample scripts from the command line:

   * **Windows**: [bootstrap-sample-db.cmd](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.cmd)
   * **Linux**: [bootstrap-sample-db.sh](https://github.com/Microsoft/sql-server-samples/blob/master/samples/features/sql-big-data-cluster/bootstrap-sample-db.sh)

   > [!TIP]
   > You can get usage instructions by running the script with no parameters.

The script restores the sample database to the SQL Server master instance and also loads data into HDFS in the storage pool.