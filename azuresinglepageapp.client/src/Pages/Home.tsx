import WeatherForecast from "../Components/WeatherForecast";
import LogoutLink from "../Components/LogoutLink";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView";
import * as React from "react";

function Home() {
    return (
        <AuthorizeView>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
        <WeatherForecast />
        </AuthorizeView>    
    );
}

export default Home;