
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-
ssver15-xxxx-xxxx-xxx.md)] 

The following article provides reference for the **sql** commands in the 
**azdata** tool. For more information about other **azdata** commands, see 
[azdata reference](reference-azdata.md)

# azdata bdc control status
## Commands
|     |     |
| --- | --- |
[azdata bdc control status show](#azdata-bdc-control-status-show) | Control status.
## azdata bdc control status show
Control status.
```bash
azdata bdc control status show 
```
### Examples
Get status of control.
```bash
azdata bdc control status show
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