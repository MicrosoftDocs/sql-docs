
External script processes run in the context of low-privilege local user accounts. Running these processes in individual low-privilege accounts has the following benefits:

+ Reduces privileges of the external script runtime processes on the host computer
+ Provides isolation between core engine processes and sessions of R and Python

As part of setup, a Windows *user account pool* called **SQLRUserGroup** is created that contains the local user accounts required for running external runtime processes. By default, this group as permissions to use executables, libraries, and built-in datasets in the program folder for R and Python under SQL Server. 

+ **SQLRUserGroup** is linked to a specific instance. A separate pool of worker accounts is needed for each instance on which machine learning has been enabled. Accounts cannot be shared between instances.

+ Worker account names in the pool are of the format SQLInstanceName*nn*. For example, if you are using the default instance for machine learning, the user account pool supports account names such as MSSQLSERVER01, MSSQLSERVER02, and so forth.

+ The size of the user account pool is static and the default value is 20. The number of external runtime sessions that can be launched simultaneously is limited by the size of this user account pool. To change this limit, see [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../../advanced-analytics/administration/modify-user-account-pool.md).

To protect sensitive resources on SQL Server, you can define an access control list (ACL) that deny access to **SQLRUserGroup**. Conversely, you could also grant permissions to local data resources that exist on the local host, apart from SQL Server itself. 

By default, the **SQLRUserGroup** does not have a login or permissions in SQL Server. Should worker accounts require a login for data access, you must create it yourself. A scenario that specifically calls for the creation of a login is to support requests from a script in execution, for data or operations on the database engine instance, when the user identity is a Windows user and the connection string specifies a trusted user. For more information, see [Add SQLRUserGroup as a database user](../../advanced-analytics/security/add-sqlrusergroup-to-database.md).
