
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-
ssver15-xxxx-xxxx-xxx.md)] 

The following article provides reference for the **sql** commands in the 
**azdata** tool. For more information about other **azdata** commands, see 
[azdata reference](reference-azdata.md)

# azdata sql
## Commands
|     |     |
| --- | --- |
[azdata sql shell](#azdata-sql-shell) | The SQL DB CLI allows the user to interact with SQL Server via T-SQL.
[azdata sql query](#azdata-sql-query) | The query command allows execution of a T-SQL query.
## azdata sql shell
The SQL DB CLI allows the user to interact with SQL Server via T-SQL.
```bash
azdata sql shell 
```
### Examples
Example command line to start the interactive experience.
```bash
azdata sql shell
```
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata sql query
The query command allows execution of a T-SQL query.
```bash
azdata sql query --database -d 
                 -q
```
### Examples
Select the list of tables names.  Database defaults to master.
```bash
azdata sql query 'SELECT name FROM SYS.TABLES'
```
### Required Parameters
#### `--database -d`
Database to run query in.  Default is master.
#### `-q`
T-SQL query to execute.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.