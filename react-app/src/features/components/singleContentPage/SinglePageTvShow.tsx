import React, { useEffect, useState } from "react";
import {  useParams } from "react-router-dom";
import { unavailable } from "../../../images/DefaultImages";
import "./singlePage.css";
import Myloader from "react-spinners/ClipLoader";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../app/stores/store";
import { TvShow } from "../../../app/models/tvShow";
import TvShowCarousel from "../carousel/TvShowCarousel";

const SinglePageTvShow = () => {
    const { tvShowStore } = useStore();
    const {  loadTvShow, loadingInitialTvShow } = tvShowStore;
    const { id } = useParams<{ id: undefined }>();
    const[tvShow, setTvShow] = useState<TvShow>();

  // eslint-disable-next-line
  const [color, setColor] = useState("grey");

  useEffect(() => {
    window.scroll(0,0)
    if (id) loadTvShow(id).then(tvShow => setTvShow(tvShow!))
}, [id, loadTvShow]);

var base64Flag = 'data:image/jpeg;base64,';
var img = base64Flag + tvShow?.image;
var imgCarousel = base64Flag + tvShow?.imageCarousel; 
  return (
    <>
      {!loadingInitialTvShow ? (
        <>
          <div>
            {tvShow && (
              <div
                className="open__modal"
                style={{
                  backgroundImage: `url(${imgCarousel})`,
                }}
              >
                <img
                  className="poster__img"
                  src={
                    tvShow.image
                      ? `${img}`
                      : unavailable
                  }
                  alt=""
                />
                <img
                  className="backdrop__img"
                  src={
                    tvShow.imageCarousel
                      ? `${imgCarousel}`
                      : unavailable
                  }
                  alt=""
                />

                <div className="open__detailsPage">
                  <h3>{tvShow.title}</h3>
                  <div
                    style={{
                      zIndex: "1000",
                      marginTop: "10px",
                      textAlign: "left",
                    }}
                    className="year"
                  >
                    {(
                      tvShow.releaseYear ||
                      "-----"
                    ).substring(0, 4)}{" "}
                    .
                    <b className="title_me">
                      Tv Series .
                    </b>
                    <b className="vote_back">
                      <b className="tmdb">TMDB</b>
                      <b className="vote_ave">-⭐{tvShow.rating}</b>
                    </b>
                  </div>
                  <h5
                    style={{
                      display: "flex",
                      fontSize: "12px",
                    }}
                    className="genreList"
                  >
                    {tvShow.genres!.map((genre, i) => {
                      return (
                        <p
                          key={genre.genreId}
                          style={{ fontSize: "13px", marginLeft: "6px" }}
                          className="mygenre"
                        >
                          {i > 0 && ", "}
                          {genre.name}
                        </p>
                      );
                    })}
                  </h5>

                  {/* <div className="videopage">
                    {content && (
                      <SingleVideoPage trailer={video} title={content.title} />
                    )}
                  </div> */}
                  <div className="overview">
                    <p>{tvShow.description}</p>
                  </div>
                  <div className="other_lists">
                    <ul>
                      <li>
                        DURATION:{" "}
                        <span>
                          {`${tvShow.episodeLength} min episodes`}
                        </span>
                      </li>
                        <li>
                          SEASONS: <span>{tvShow.seasons}</span>
                        </li>

                      <li>
                      </li>
                        <li>
                          RELEASE DATE: <span>{tvShow.releaseYear.substring(0,10)}</span>
                        </li>
                    </ul>
                  </div>
                </div>
              </div>
            )}
          </div>
          <div className="all__cast px-5 pt-5">
            <div className="cast__title">
              <h2>Cast</h2>
            </div>
            <div>
              <TvShowCarousel tvShow={tvShow} />
            </div>
          </div>
        </>
      ) : (
        <div className="load_app" style={{ height: "500px" }}>
          <Myloader color={color} size={60} />
          <p
            className="pt-4 text-secondary text-loading"
            style={{ textTransform: "capitalize", fontSize: "1rem" }}
          >
            Loading Please Wait...
          </p>
        </div>
      )}
    </>
  );
};

export default observer(SinglePageTvShow);