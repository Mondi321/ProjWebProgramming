import Heading from "../header/Heading";
import "./MainNav.css";
import React from "react";
import { Link } from "react-router-dom";
import HomeIcon from "../../../images/home-icon.svg";
import MovieIcon from "../../../images/movie-icon.svg";
import TheatersIcon from "../../../images/series-icon.svg";
import TicketIcon from "../../../images/ticket.svg";
import UserIcon from "../../../images/user.svg";
import ContactIcon from "../../../images/contact.svg";
import $ from 'jquery';
import { useStore } from "../../../app/stores/store";
import { NavDropdown } from "react-bootstrap";
import { observer } from "mobx-react-lite";
import ReviewModal from "../../pages/ReviewModal";

$(function () {
  $(document).on("scroll", function () {
    var $nav = $(".navbar");
    $nav.toggleClass("scrolled", $(this).scrollTop()! > $nav.height()!);
  });
});

const MainNav = () => {

  const { userStore: { user, logout }, commonStore } = useStore();
  const {modalShow, setModalShow} = commonStore;

  return (
    <>
      <nav className="navbar navbar-expand navbar-light fixed-top">
        <Link className="navbar-brand" to="/">
          <Heading />
        </Link>

        <div className="collapse navbar-collapse">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item active  nav__link">
              <Link className="nav-link" to="/home">
                <img
                  src={HomeIcon}
                  style={{
                    fontSize: "17px",
                    marginBottom: "5px",
                    marginRight: "0px",
                  }}
                  alt=""
                />
                Home
              </Link>
            </li>
            <li className="nav-item  nav__link">
              <Link className="nav-link" to="/all-movies">
                <img
                  src={MovieIcon}
                  style={{
                    fontSize: "17px",
                    marginBottom: "2px",
                    marginRight: "1px",
                  }}
                  alt=""
                />
                Movies
              </Link>
            </li>
            <li className="nav-item nav__link">
              <Link className="nav-link" to="/all-series">
                <img
                  src={TheatersIcon}
                  style={{
                    fontSize: "17px",
                    marginBottom: "5px",
                    marginRight: "1px",
                  }}
                  alt=""
                />
                TvSeries
              </Link>
            </li>
            <li className="nav-item nav__link">
              <a href="https://localhost:7115/Identity/Account/Login?ReturnUrl=%2FMovies" className="nav-link" >
                <img
                  src={TicketIcon}
                  style={{
                    fontSize: "17px",
                    marginBottom: "5px",
                    marginRight: "1px"
                  }}
                  alt=""
                />
                eTickets
              </a>
            </li>
            <li className="nav-item nav__link">
              <Link className="nav-link" to="/contactus">
                <img
                  src={ContactIcon}
                  style={{
                    fontSize: "17px",
                    marginBottom: "5px",
                    marginRight: "1px",
                  }}
                  alt=""
                />
                Contact
              </Link>
            </li>


          </ul>
          <div className="userInformation">
            <img src={UserIcon} alt="" />
            <NavDropdown
              id="nav-dropdown-dark-example"
              title={user?.firstName}
              menuVariant="dark"
            >
              {user?.roli.includes('Admin') && (
                <NavDropdown.Item href="https://localhost:7115/Identity/Account/Login?ReturnUrl=%2FMovies">Dashboard</NavDropdown.Item>
              )}
              <NavDropdown.Item onClick={() => setModalShow(true)}>Write a Review</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item onClick={logout}>
                Logout
              </NavDropdown.Item>
            </NavDropdown>

          </div>
        </div>
      </nav>
      <ReviewModal
                modalShow={modalShow}
                setModalShow={setModalShow}
            />
    </>
  );
};

export default observer(MainNav);