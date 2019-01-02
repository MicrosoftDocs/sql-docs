//<snippetFS_CPP_Enable>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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
    /// This parameter is the type of ODBC handle that is passed in
    /// the next parameter.
    ///</param>
    /// <param name="handle">
    /// This parameter is the handle on which the error occurred on.
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


void main()
{
    TCHAR *sqlDBQuery =
        TEXT("EXEC sp_filestream_configure ")
        TEXT("@enable_level = 3;");
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);
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
//</snippetFS_CPP_Enable>

//<snippetFS_CPP_CreateDB>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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
    /// This parameter is the type of ODBC handle that is passed in
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


void main()
{
    TCHAR *sqlDBQuery =
        TEXT(" CREATE DATABASE Archive ")
        TEXT(" ON")
        TEXT(" PRIMARY ( NAME = Arch1,")
        TEXT(" FILENAME = 'c:\\data\\archdat1.mdf'),")
        TEXT(" FILEGROUP FileStreamGroup1")
        TEXT(" CONTAINS FILESTREAM")
        TEXT(" ( NAME = Arch3,")
        TEXT(" FILENAME = 'c:\\data\\filestream1')")
        TEXT(" LOG ON ")
        TEXT(" ( NAME = Archlog1,")
        TEXT(" FILENAME = 'c:\\data\\archlog1.ldf')");
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hstmt);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);
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
//</snippetFS_CPP_CreateDB>

//<snippetFS_CPP_CreateTable>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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


void main()
{
    TCHAR *sqlDBQuery =
        TEXT("CREATE TABLE Archive.dbo.Records (")
        TEXT(" [Id] [uniqueidentifier]")
        TEXT(" ROWGUIDCOL NOT NULL UNIQUE,")
        TEXT(" [SerialNumber] INTEGER UNIQUE,")
        TEXT(" [Chart] VARBINARY(MAX) FILESTREAM NULL)");
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hstmt);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);
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
//</snippetFS_CPP_CreateTable>

//<snippetFS_CPP_InsertNULL>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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


void main()
{
    TCHAR *sqlDBQuery =
       TEXT("INSERT INTO Archive.dbo.Records")
       TEXT(" VALUES (newid (), 1, NULL)");
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hstmt);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);
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
//</snippetFS_CPP_InsertNULL>

//<snippetFS_CPP_InsertZero>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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


void main()
{
    TCHAR *sqlDBQuery =
       TEXT("INSERT INTO Archive.dbo.Records")
       TEXT(" VALUES (newid (), 2, CAST ('' as varbinary(max)))");
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hstmt);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);
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
//</snippetFS_CPP_InsertZero>

//<snippetFS_CPP_InsertData>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


/// <summary>
///This class iterates though the ODBC error queue and prints all of the
///accumulated error messages to the console.
/// </summary>

class ODBCErrors
{
private:
    int         m_iLine;    //Source code line on which the error occurred
    SQLSMALLINT m_type;     //Type of handle on which the error occurred
    SQLHANDLE   m_handle;   //ODBC handle on which the error occurred.

public:
    /// <summary>
    ///Default constructor for the ODBCErrors class.
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
    /// This parameter is the handle on which the error occurred on.
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


void main()
{
    TCHAR *sqlDBQuery =
       TEXT("INSERT INTO Archive.dbo.Records")
       TEXT(" VALUES (newid (), 3, ")
       TEXT(" CAST('Seismic Data' as varbinary(max)))");
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hstmt);

        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);
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
//</snippetFS_CPP_InsertData>

//<snippetFS_CPP_PathName>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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
    /// This parameter is the handle on which the error occurred on.
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


void main()
{
    TCHAR *sqlDBQuery =
        TEXT("SELECT Chart.PathName()")
        TEXT(" FROM Archive.dbo.Records")
        TEXT(" WHERE SerialNumber = 3");

	SQLTCHAR srcFilePath[1024];
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc  = SQL_NULL_HANDLE;
    SQLHANDLE hstmt   = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        if ( SQLAllocHandle(SQL_HANDLE_STMT, hdbc, &hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hstmt);

        //These statements execute the query and read the path from the
        //returned data.
        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        if ( SQLFetch(hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        SQLINTEGER cbsrcFilePath = sizeof(srcFilePath)/sizeof(SQLTCHAR);

    	if ( SQLGetData(hstmt, 1,
            SQL_C_TCHAR,
            srcFilePath,
            sizeof(srcFilePath),
            &cbsrcFilePath) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        wprintf_s(TEXT("Path = %s\n"), srcFilePath);
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
//</snippetFS_CPP_PathName>

//<snippetFS_CPP_GET_TRANSACTION_CONTEXT>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>


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
    /// This parameter is the handle on which the error occurred on.
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


void main()
{
    TCHAR *sqlDBQuery =
       TEXT("INSERT INTO Archive.dbo.Records(Id, SerialNumber, Chart)")
       TEXT(" OUTPUT GET_FILESTREAM_TRANSACTION_CONTEXT(), inserted.Chart.PathName()")
       TEXT("VALUES (newid (), 4, CONVERT(VARBINARY, '**Temp**'))");

	SQLCHAR transactionToken[32];
    
    SQLHANDLE henv = SQL_NULL_HANDLE;
    SQLHANDLE hdbc              = SQL_NULL_HANDLE;
    SQLHANDLE hstmt             = SQL_NULL_HANDLE;

    try
    {
        //These statements initialize ODBC for the client application and
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

        //These statements execute the query and read the path from the
        //returned data.
        if ( SQLExecDirect(hstmt, sqlDBQuery, SQL_NTS) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        if ( SQLFetch(hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        SQLINTEGER cbTransactionToken = sizeof(transactionToken);

    	if ( SQLGetData(hstmt, 1,
            SQL_C_BINARY,
            transactionToken,
            sizeof(transactionToken),
            &cbTransactionToken) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        wprintf_s(TEXT("Transaction Token = "));

        for (int i = 0; i < cbTransactionToken; i++)
            wprintf_s(TEXT("%X "), transactionToken[i]);

        wprintf_s(TEXT("\n"));

        SQLEndTran(SQL_HANDLE_STMT, hstmt, SQL_COMMIT);
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
//</snippetFS_CPP_GET_TRANSACTION_CONTEXT>

//<snippetFS_CPP_WriteBLOB>
#include <windows.h>
#include <sql.h>
#include<sqltypes.h>
#include<sqlext.h>
#include <stdio.h>
#include <sqlncli.h>

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
//</snippetFS_CPP_WriteBLOB>

//<snippetFS_CPP_FSCTL>
#include <windows.h>
#include<sqlext.h>
#include <stdio.h>
#include <sqlncli.h>
#include <tchar.h>
#include <strsafe.h>

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

/// <summary>
///This function waits for the overlapped IO operation to complete; TRUE
///is returned if the function completes successfully and FALSE is
///returned if an error occurs.
///</summary>

BOOL WaitForIOToComplete(HANDLE srcHandle, OVERLAPPED *pOverLapped, LPDWORD pBytes)
{
    DWORD bError = GetLastError();

    if ( bError == ERROR_HANDLE_EOF )
        return TRUE;

    //If GetLastError() does not return EOF or IO PENDING then
    //an error has occured.
    if ( bError != ERROR_IO_PENDING )
        return FALSE;

    do
    {
        //In a real application you would perform non-blocking
        //processing here.
        SleepEx(0, TRUE);
        if ( GetOverlappedResult(srcHandle, pOverLapped, pBytes, FALSE) == 0 )
            return FALSE;
    } while( GetLastError() == ERROR_IO_PENDING );

    return TRUE;
}

/// <summary>
///This function performs a partial update of the inserted
///FILESTREAM BLOB.
///</summary>

BOOL UpdateBLOB(LPTSTR srcFilePath, LPBYTE transactionToken, SQLINTEGER cbTransactionToken)
{
    BOOL bRetCode        = FALSE;
    HANDLE srcHandle     = INVALID_HANDLE_VALUE;
    TCHAR *szErrMsgSrc   = TEXT("Error opening FILESTREAM BLOB.");
    TCHAR *szErrMsgRead  = TEXT("Error reading source file.");
    TCHAR *szErrMsgWrite = TEXT("Error writing SQL file.");
    TCHAR *szErrMsgIOCtl = TEXT("Error calling DeviceIoControl.");

    try
    {
        //Obtain a handle to the FILESTREAM BLOB data.
        srcHandle =  OpenSqlFilestream(srcFilePath,
                                       ReadWrite,
                                       0,
                                       transactionToken,
                                       cbTransactionToken,
                                       0);
        if ( srcHandle == INVALID_HANDLE_VALUE )
            throw szErrMsgSrc;

        //The DeviceIoControl API will fill in an return this
        //structure. This structure can be used to implement
        //overlapped IO.

    	OVERLAPPED overlapped;
    	ZeroMemory(&overlapped, sizeof (overlapped));

        DWORD bytesRead = 0;
        DWORD bytesWritten = 0;

        //If ReadFile is called it will return EOF because the
        //FILESTREAM BLOB data has not been copied to the open
        //File handle. Issuing This IOCTL will cause the server
        //to copy the BLOB data to the file referenced by the
        //srcHandle.

        if ( !DeviceIoControl(srcHandle,
                              FSCTL_SQL_FILESTREAM_FETCH_OLD_CONTENT,
                              NULL,
                              0,
                              NULL,
                              0,
                              NULL,
                              &overlapped))
        {
            throw szErrMsgIOCtl;
        }        

        //In a real application you would read more information than a
        //few bytes. This small sample is provided to illustrate how to
        //perform partial reads and writes starting at an offset within
        //the FILESTREAM BLOB.
        BYTE buffer[8];

        overlapped.Offset = 2;

        if ( ReadFile(srcHandle, buffer, 4, &bytesRead, &overlapped) == 0 )
            if ( WaitForIOToComplete(srcHandle, &overlapped, &bytesRead) == 0 )
                throw szErrMsgRead;

        //Change the word Temp currently stored in the BLOB to the 
        //word Done. Write the new word back to the FILESTREAM BLOB.
        buffer[0] = 'D';
        buffer[1] = 'o';
        buffer[2] = 'n';
        buffer[3] = 'e';

        if ( WriteFile(srcHandle, buffer, bytesRead, &bytesWritten, &overlapped) == 0 )
            if ( WaitForIOToComplete(srcHandle, &overlapped, &bytesWritten) == 0 )
                throw szErrMsgWrite;

        bRetCode = TRUE;
    }
    catch( TCHAR *szErrMsg )
    {
        LPTSTR lpMsgBuf = NULL;

        FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | 
                      FORMAT_MESSAGE_FROM_SYSTEM |
                      FORMAT_MESSAGE_IGNORE_INSERTS,
                      NULL,
                      GetLastError(),
                      MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
                      (LPTSTR) &lpMsgBuf,
                      0, NULL );

        wprintf_s(szErrMsg );
        wprintf_s(lpMsgBuf);
        bRetCode = FALSE;

        LocalFree(lpMsgBuf);
    }

    if ( srcHandle != INVALID_HANDLE_VALUE )
        CloseHandle(srcHandle);

    return bRetCode;
}

void main()
{
    //This query inserts a new record, reads the FILESTREAM path of the
    //inserted record and returns the current transaction context.

    TCHAR *sqlDBQuery =
       TEXT("INSERT INTO Archive.dbo.Records(Id, SerialNumber, Chart)")
       TEXT(" OUTPUT GET_FILESTREAM_TRANSACTION_CONTEXT(), inserted.Chart.PathName()")
       TEXT("VALUES (newid (), 35, CONVERT(VARBINARY, '**Temp**'))");

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

         //This code assumes that an ODBC User DSN has been created
         //with the following settings:
         //
         //Driver:      Sql Server Native Client 10.0
         //Name:        Sql Server FILESTREAM
         //Description: Sql Server FILESTREAM
         //Server: .
         //Change Default Database to: Archive

         if ( SQLConnect(hdbc, TEXT("Sql Server FILESTREAM"),
                         SQL_NTS, NULL, 0, NULL, 0) <= 0 )
             throw new ODBCErrors(__LINE__, SQL_HANDLE_DBC, hdbc);

        //FILESTREAM requires that all read and write operations occur
        //within a transaction. The ODBC driver will not automatically
        //COMMIT the transaction it BEGIN if SQL_AUTOCOMMIT_OFF is set.

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
        TCHAR srcFilePath[1024];
        SQLINTEGER cbsrcFilePath;

        if ( SQLGetData(hstmt, 2, SQL_C_TCHAR, srcFilePath, sizeof(srcFilePath), &cbsrcFilePath) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

    	if ( SQLCloseCursor(hstmt) != SQL_SUCCESS )
            throw new ODBCErrors(__LINE__, SQL_HANDLE_STMT, hstmt);

        SQLUSMALLINT mode = SQL_ROLLBACK;

        //Update the FILESTREAM BLOB data from **Temp** to **Done**.
        if ( UpdateBLOB(srcFilePath,
            transactionToken,
            cbTransactionToken) == TRUE )
            mode = SQL_COMMIT;

        //Commit the TRANSACTION if the BLOB was successfully udpated;
        //ROLLBACK the TRANSACTION if it was not.
        SQLTransact(henv, hdbc, mode);
    }
    catch(ODBCErrors *pErrors)
    {
        pErrors->Print();
        delete pErrors;
    }

    //Free and clean up the ODBC data handles that were initially created.
    if ( hstmt != SQL_NULL_HANDLE )
        SQLFreeHandle(SQL_HANDLE_STMT, hstmt);

    if ( hdbc != SQL_NULL_HANDLE )
        SQLDisconnect(hdbc);

    if ( hdbc != SQL_NULL_HANDLE )
        SQLFreeHandle(SQL_HANDLE_DBC, hdbc); 

    if ( henv != SQL_NULL_HANDLE )
        SQLFreeHandle(SQL_HANDLE_ENV, henv);
}
//</snippetFS_CPP_FSCTL>
