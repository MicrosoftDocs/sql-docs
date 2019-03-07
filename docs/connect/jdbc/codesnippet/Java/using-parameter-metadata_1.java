public static void getParameterMetaData(Connection con) {
    try(CallableStatement cstmt = con.prepareCall("{call HumanResources.uspUpdateEmployeeHireInfo(?, ?, ?, ?, ?)}");) {
        ParameterMetaData pmd = cstmt.getParameterMetaData();
        int count = pmd.getParameterCount();
        for (int i = 1; i <= count; i++) {
            System.out.println("TYPE: " + pmd.getParameterTypeName(i) + " MODE: " + pmd.getParameterMode(i));
        }
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
