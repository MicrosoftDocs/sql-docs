#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>
#include <msodbcsql.h>

#define COPYBUFFERSIZE 4096

/// <summary>
///This class iterates though the ODBC error queue and prints all of the
///accumulated error messages to the console.
/// </summary>

class ODBCErrors
{
private:
    int         m_iLine;    //Source code line on which the error occurred
    SQLSMALLINT m_type;     //Type of handle on which the error occurred
    SQLHANDLE   m_handle;   //ODBC handle on which the error occurred

public:
    /// <summary>
    ///Default constructor for the ODBCErrors class
    ///</summary>

    ODBCErrors()
    {
        m_iLine  = -1;
        m_type   = 0;
        m_handle = SQL_NULL_HANDLE;
    }

    /// <summary>
    ///Constructor for the ODBCErrors class
    /// </summary>
    /// <param name="iLine">
    /// This parameter is the source code line
    /// at which the error occurred.
    ///</param>
    /// <param name="type">
    /// This parameter is the type of ODBC handle passed in
    /// the next parameter.
    ///</param>
    /// <param name="handle">
    /// This parameter is the handle on which the error occurred.
    ///</param>

    ODBCErrors(int iLine, SQLSMALLINT type, SQLHANDLE handle)
    {
        m_iLine  = iLine;
        m_type   = type;
        m_handle = handle;
    }

    ///<summary>
    /// This method iterates though the error stack for the handle passed
    /// into the constructor and displays those errors on the console.
    ///</summary>

    void Print()
    {
        SQLSMALLINT i = 0, len = 0;
        SQLINTEGER  native;
        SQLTCHAR    state[9], text[256];
        SQLRETURN   sqlReturn = SQL_SUCCESS;

        if ( m_handle == SQL_NULL_HANDLE )
        {
            wprintf_s(TEXT("The error handle is not a valid handle.\n"), m_iLine);
            return;
        }

        wprintf_s(TEXT("Error Line(%d)\n"), m_iLine);

        while( sqlReturn == SQL_SUCCESS )
        {
            len = 0;

            sqlReturn = SQLGetDiagRec(
                m_type,
                m_handle,
                ++i,
                state,
                &native,
                text,
                sizeof(text)/sizeof(SQLTCHAR),
                &len);

            if ( SQL_SUCCEEDED(sqlReturn) )
                wprintf_s(TEXT("Error(%d, %ld, %s) : %s\n"), i, native, state, text);
        }
    }
};


BOOL CopyFileToSQL(LPTSTR srcFilePath, LPTSTR dstFilePath, LPBYTE transactionToken, SQLINTEGER cbTransactionToken)
{
	BOOL bRetCode = FALSE;

	HANDLE srcHandle = INVALID_HANDLE_VALUE;
	HANDLE dstHandle = INVALID_HANDLE_VALUE;
    BYTE   buffer[COPYBUFFERSIZE] = { 0 };

    TCHAR *szErrMsgSrc   = TEXT("Error opening source file.");
    TCHAR *szErrMsgDst   = TEXT("Error opening destFile file.");
    TCHAR *szErrMsgRead  = TEXT("Error reading source file.");
    TCHAR *szErrMsgWrite = TEXT("Error writing SQL file.");

    try
    {
	    if ( (srcHandle = CreateFile(
            srcFilePath,
            GENERIC_READ,
            FILE_SHARE_READ,
            NULL,
            OPEN_EXISTING,
            FILE_FLAG_SEQUENTIAL_SCAN,
            NULL)) == INVALID_HANDLE_VALUE )
            throw szErrMsgSrc;

    	if ( (dstHandle =  OpenSqlFilestream(
            dstFilePath,
            Write,
            0,
            transactionToken,
            cbTransactionToken,
            0)) == INVALID_HANDLE_VALUE)
            throw szErrMsgDst;

        DWORD bytesRead = 0;
        DWORD bytesWritten = 0;

    	do
        {
            if ( ReadFile(srcHandle, buffer, COPYBUFFERSIZE, &bytesRead, NULL) == 0 )
                throw szErrMsgRead;

    		if (bytesRead > 0)
            {
        		if ( WriteFile(dstHandle, buffer, bytesRead, &bytesWritten, NULL) == 0 )
                    throw szErrMsgWrite;
            }
		} while (bytesRead > 0);

        bRetCode = TRUE;
	}
    catch( TCHAR *szErrMsg )
    {
        wprintf_s(szErrMsg);
        bRetCode = FALSE;
    }

    if ( srcHandle != INVALID_HANDLE_VALUE )
        CloseHandle(srcHandle);

    if ( dstHandle != INVALID_HANDLE_VALUE )
    	CloseHandle(dstHandle);

    return bRetCode;
}

void main()
{
    TCHAR *sqlDBQuery =
       TEXT("INSERT INTO Archive.dbo.Records(Id, SerialNumber, Chart)")
       TEXT(" OUTPUT GET_FILESTREAM_TRANSACTION_CONTEXT(), inserted.Chart.PathName()")
       TEXT("VALUES (newid (), 5, CONVERT(VARBINARY, '**Temp**'))");

	SQLCHAR transactionToken[32];
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc              = SQL_NULL_HANDLE;
    SQLHANDLE hstmt             = SQL_NULL_HANDLE;

    try
    {
        //These statements Initialize ODBC for the client application and
        //connect to the database.

        if ( SQLAllocHandle(SQL_HANDLE_ENV, SQL_NULL_HANDLE, &henv) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_ENV, henv);

        if ( SQLSetEnvAttr(henv, SQL_ATTR_ODBC_VERSION,(void*)SQL_OV_ODBC3, NULL) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_ENV, henv);

        if ( SQLAllocHandle(SQL_HANDLE_DBC, henv, &hdbc) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_ENV, henv);

	    //This code assumes that the dataset name "Sql Server FILESTREAM"
	    //has been previously created on the client computer system. An
        //ODBC DSN is created with the ODBC Data Source item in
        //the Windows Control Panel.

	    if ( SQLConnect(hdbc, TEXT("Sql Server FILESTREAM"),
                SQL_NTS, NULL, 0, NULL, 0) <= 0 )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hdbc);

        //FILESTREAM requires that all read and write operations occur
        //within a transaction.
        if ( SQLSetConnectAttr(hdbc,
            SQL_ATTR_AUTOCOMMIT,
            (SQLPOINTER)SQL_AUTOCOMMIT_OFF,
            SQL_IS_UINTEGER) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hdbc);

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hdbc);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        //Retrieve the transaction token.
        if ( SQLFetch(hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        SQLINTEGER cbTransactionToken = sizeof(transactionToken);

    	if ( SQLGetData(hstmt, 1,
            SQL_C_BINARY,
            transactionToken,
            sizeof(transactionToken),
            &cbTransactionToken) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        //Retrieve the file path for the inserted record.

        TCHAR dstFilePath[1024];
        SQLINTEGER cbDstFilePath;

        if ( SQLGetData(hstmt, 2, SQL_C_TCHAR, dstFilePath, sizeof(dstFilePath), &cbDstFilePath) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

    	if ( SQLCloseCursor(hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        SQLUSMALLINT mode = SQL_ROLLBACK;

        if ( CopyFileToSQL(
            TEXT("C:\\Users\\Data\\chart1.jpg"),
            dstFilePath,
            transactionToken,
            cbTransactionToken) == TRUE )
            mode = SQL_COMMIT;

        SQLTransact(henv, hdbc, mode);
    }
    catch(ODBCErrors *pErrors)
    {
        pErrors->Print();
        delete pErrors;
    }

    if ( hstmt != SQL_NULL_HANDLE )
        SQLFreeHandle(SQL_HANDLE_STMT, hstmt);

    if ( hdbc != SQL_NULL_HANDLE )
        SQLDisconnect(hdbc);

    if ( hdbc != SQL_NULL_HANDLE )
        SQLFreeHandle(SQL_HANDLE_DBC, hdbc); 

    if ( henv != SQL_NULL_HANDLE )
        SQLFreeHandle(SQL_HANDLE_ENV, henv);
}