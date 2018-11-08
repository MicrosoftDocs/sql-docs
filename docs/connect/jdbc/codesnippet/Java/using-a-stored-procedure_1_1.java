public static void executeStoredProcedure(Connection con) {
    try(CallableStatement cstmt = con.prepareCall("{? = call dbo.CheckContactCity(?)}");) {
        cstmt.registerOutParameter(1, java.sql.Types.INTEGER);
        cstmt.setString(2, "Atlanta");
        cstmt.execute();
        System.out.println("RETURN STATUS: " + cstmt.getInt(1));
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
