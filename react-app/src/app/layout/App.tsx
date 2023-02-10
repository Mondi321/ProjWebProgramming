import React, { useEffect, useState } from 'react';
import Intro from './Intro';
import { Route, Switch } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import Forms from '../../features/users/Forms';
import NotFound from '../../features/errors/NotFound';
import ServerError from '../../features/errors/ServerError';
import { ToastContainer } from 'react-toastify';
import { useStore } from '../stores/store';
import TestErrors from '../../features/errors/TestError';
// import HomeNav from '../../features/components/homeNav/HomeNav';
import MainNav from '../../features/components/mainNavbar/MainNav';
import Home from '../../features/pages/HomePage';
import mySvg from "../../images/hbo-max.svg";
import Myloader from "react-spinners/ClipLoader";
import "../../../node_modules/touch-loader/touchLoader";
import "./app.css";
import Footer from '../../features/components/footer/Footer';
import LastFooter from '../../features/components/copyWrite/LastFooter';
import BottomNav from '../../features/components/mainNavbar/BottomNav';
import Movies from '../../features/pages/Movies';
import TvSeries from '../../features/pages/TvSeries';
import SinglePage from '../../features/components/singleContentPage/SinglePage';
import SinglePageTvShow from '../../features/components/singleContentPage/SinglePageTvShow';
import ContactForm from '../../features/pages/ContactForm';

function App() {
  const { commonStore, userStore } = useStore();
  // eslint-disable-next-line
  let [color, setColor] = useState("grey");
  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => commonStore.setAppLoaded());
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore])

  // if (!commonStore.appLoaded) return <LoadingComponent />

  return (
    <>
      {commonStore.appLoaded ? (
        <>
          <ToastContainer position='bottom-right' hideProgressBar />
          <Switch>
            <Route exact path='/' component={Intro} />

            <Route
              path="(/sign-in|/sign-up)"
              render={() => (
                <Forms />
              )}
            />

            <Route
              path='/home' render={() => (
                <>
                  <div className="myApp">
                    <MainNav />
                    <Home />
                    <Footer />
                    <BottomNav />
                    <LastFooter />
                  </div>

                </>
              )}
            />

            <Route
              path='/all-movies' render={() => (
                <>
                  <div className="myApp">
                    <MainNav />
                    <Movies />
                    <Footer />
                    <BottomNav />
                    <LastFooter />
                  </div>

                </>
              )}
            />

            <Route
              path='/movies/:id' render={() => (
                <>
                  <div className="myApp">
                    <MainNav />
                    <SinglePage />
                    <Footer />
                    <BottomNav />
                    <LastFooter />
                  </div>

                </>
              )}
            />


            <Route
              path='/tvshows/:id' render={() => (
                <>
                  <div className="myApp">
                    <MainNav />
                    <SinglePageTvShow />
                    <Footer />
                    <BottomNav />
                    <LastFooter />
                  </div>

                </>
              )}
            />
            <Route
              path='/all-series' render={() => (
                <>
                  <div className="myApp">
                    <MainNav />
                    <TvSeries />
                    <Footer />
                    <BottomNav />
                    <LastFooter />
                  </div>

                </>
              )}
            />

            <Route
              path='/contactus' render={() => (
                <div className="myApp">
                <MainNav />
                <ContactForm />
              </div>
              )}
            />

            <Route
              path='/server-error' component={ServerError}
            />

            <Route
              path='/errors' component={TestErrors}
            />
            <Route component={NotFound} />
          </Switch>
        </>

      ) : (
        <div className="load_app" style={{ height: "400px" }}>
          <Myloader
            color={color}
            size={80}
            className="m__load"
            speedMultiplier={1.5}
          />
          <img src={mySvg} alt="" width="300" className="logo2 pt-4" />{" "}
        </div>
      )}

    </>
  );
}

export default observer(App);
