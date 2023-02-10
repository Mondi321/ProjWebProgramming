import "./HomeNav.css";
import React, { useEffect, useState } from "react";
import AliceCarousel from "react-alice-carousel";
import "react-alice-carousel/lib/alice-carousel.css";
import { useHistory } from "react-router-dom";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import Myloader from "react-spinners/PuffLoader";
// const handleDragStart = (e) => e.preventDefault();

const HomeNav = () => {
  const { movieStore } = useStore();
  const { movieRegistry, loadMovies, moviesCarousel, loadingInitial } = movieStore;
  const history = useHistory();
    // eslint-disable-next-line
    let [color, setColor] = useState("grey");

  useEffect(() => {
    if (movieRegistry.size <= 1) loadMovies();
  }, [movieRegistry.size, loadMovies])


  const handleClick = (id: string) => {
    history.push(`/movies/${id}/`);
  };
  const allMovies = moviesCarousel.map((movie) => {
    var base64Flag = 'data:image/jpeg;base64,';
    var img = base64Flag + movie.imageCarousel;

    return (
      <div
        key={movie.movieId}
        className="main__nav"
        style={{
          backgroundImage: `url(${img})`,
        }}
      >
        <div className="nav">
          <h3>{movie.title}</h3>
          <h5 style={{ color: "#abb7c4" }}>
            Movie
          </h5>

          <p>{movie.description}</p>
          <div className="back__btn">
            <button onClick={() => handleClick(movie.movieId)}>
              LEARN MORE
            </button>
          </div>
        </div>
      </div>
    )
  });



  return (
    <>
      {!loadingInitial ? (
        <AliceCarousel
          infinite
          autoPlay
          disableButtonsControls
          disableDotsControls
          mouseTracking
          autoPlayInterval={1500}
          items={allMovies}
        //   responsive={responsive}
        />
      ) : (
        <div className="major" style={{ height: "600px" }}>
          <Myloader color={color} size={60} />
        </div>
      )}
    </>


  );
};

export default observer(HomeNav);