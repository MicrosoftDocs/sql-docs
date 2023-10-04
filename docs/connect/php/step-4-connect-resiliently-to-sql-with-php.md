---
title: "Step 4: Connect resiliently to SQL with PHP"
description: Step 4 is a demo program designed to showcase how transient errors during an attempt to connect leads to a retry.
author: David-Engel
ms.author: v-davidengel
ms.date: 05/05/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Step 4: Connect resiliently to SQL with PHP

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The demo program is designed so that a transient error during an attempt to connect leads to a retry. (Transient error codes start with the prefix '08' as listed in this [appendix](../../odbc/reference/appendixes/appendix-a-odbc-error-codes.md).) But a transient error during query command causes the program to discard the connection and create a new connection, before retrying the query command. We don't recommend or discourage this design choice. The demo program illustrates some of the design flexibility that is available to you.

The length of this code sample is due mostly to the catch exception logic.

The [sqlsrv_query()](sqlsrv-query.md) function can be used to retrieve a result set from a query against SQL Database. This function essentially accepts any query and connection object and returns a result set, which can be iterated over with the use of [sqlsrv_fetch_array()](sqlsrv-fetch-array.md).

```php

    <?php
        // Variables to tune the retry logic.
        $connectionTimeoutSeconds = 30;  // Default of 15 seconds is too short over the Internet, sometimes.
        $maxCountTriesConnectAndQuery = 3;  // You can adjust the various retry count values.
        $secondsBetweenRetries = 4;  // Simple retry strategy.
        $errNo = 0;
        $serverName = "tcp:yourdatabase.database.windows.net,1433";
        $connectionOptions = array("Database"=>"AdventureWorks",
           "Uid"=>"yourusername", "PWD"=>"yourpassword", "LoginTimeout" => $connectionTimeoutSeconds);
        $conn = null;
        $arrayOfTransientErrors = array('08001', '08002', '08003', '08004', '08007', '08S01');
        for ($cc = 1; $cc <= $maxCountTriesConnectAndQuery; $cc++) {
            // [A.2] Connect, which proceeds to issue a query command.
            $conn = sqlsrv_connect($serverName, $connectionOptions);
            if ($conn === true) {
                echo "Connection was established";
                echo "<br>";

                $tsql = "SELECT Name FROM Production.ProductCategory";
                $stmt = sqlsrv_query($conn, $tsql);
                if ($stmt === false) {
                    echo "Error in query execution";
                    echo "<br>";
                    die(print_r(sqlsrv_errors(), true));
                }
                while($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
                    echo $row['Name'] . "<br/>" ;
                }
                sqlsrv_free_stmt($stmt);
                sqlsrv_close( $conn);
                break;
            } else {
                // [A.4] Check whether the error code is on the list of allowed transients.
                $isTransientError = false;
                $errorCode = '';
                if (($errors = sqlsrv_errors()) != null) {
                    foreach ($errors as $error) {
                        $errorCode = $error['code'];
                        $isTransientError = in_array($errorCode, $arrayOfTransientErrors);
                        if ($isTransientError) {
                            break;
                        }
                    }
                }
                if (!$isTransientError) {
                    // it is a static persistent error...
                    echo("Persistent error suffered with error code = $errorCode. Program will terminate.");
                    echo "<br>";
                    // [A.5] Either the connection attempt or the query command attempt suffered a persistent error condition.
                    // Break the loop, let the hopeless program end.
                    exit(0);
                }
                // [A.6] It is a transient error from an attempt to issue a query command.
                // So let this method reloop and try again. However, we recommend that the new query
                // attempt should start at the beginning and establish a new connection.
                if ($cc >= $maxCountTriesConnectAndQuery) {
                    echo "Transient errors suffered in too many retries - $cc. Program will terminate.";
                    echo "<br>";
                    exit(0);
                }
                echo("Transient error encountered with error code = $errorCode. Program might retry by itself.");
                echo "<br>";
                echo "$cc attempts so far. Might retry.";
                echo "<br>";
                // A very simple retry strategy, a brief pause before looping.
                sleep(1*$secondsBetweenRetries);
            }
            // [A.3] All has gone well, so let the program end.
        }
    ?>
```
