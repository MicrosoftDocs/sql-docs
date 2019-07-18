
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-
ssver15-xxxx-xxxx-xxx.md)] 

The following article provides reference for the **sql** commands in the 
**azdata** tool. For more information about other **azdata** commands, see 
[azdata reference](reference-azdata.md)

# azdata bdc status
## Commands
|     |     |
| --- | --- |
[azdata bdc status show](#azdata-bdc-status-show) | Shows the status of the Big Data Cluster.
## azdata bdc status show
Shows the status of the Big Data Cluster.
```bash
azdata bdc status show 
```
### Examples
BDC status where the user is logged in.
```bash
azdata bdc status show
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