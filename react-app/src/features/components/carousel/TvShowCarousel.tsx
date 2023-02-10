import { observer } from "mobx-react-lite";
import React from "react";
import AliceCarousel from "react-alice-carousel";
import "react-alice-carousel/lib/alice-carousel.css";
import { TvShow } from "../../../app/models/tvShow";
import { noPicture } from "../../../images/DefaultImages";
import "./carousel.css";
const handleDragStart = (e:any) => e.preventDefault();

interface Props{
    tvShow: TvShow | undefined;
}

const TvShowCarousel = ({tvShow}: Props) => {



  const responsive = {
    0: {
      items: 1,
    },
    380: {
      items: 1,
    },
    512: {
      items: 2,
    },
    665: {
      items: 3,
    },
    767: {
      items: 3,
    },
    870: {
      items: 4,
    },
    1024: {
      items: 6,
    },
    1265: {
      items: 7,
    },
  };

  var base64Flag = 'data:image/jpeg;base64,';
  const items = tvShow?.actors?.map((actor) => {
    return (
      <div className="carousel__d">
        <img
          src={actor.image ? `${base64Flag+actor.image}` : noPicture}
          alt=""
          className="caro_img"
          onDragStart={handleDragStart}
        />
        <div className="caro__details">
          <h6 className="cast__name">{actor.firstName}</h6>
          {/* <h6 className="character">{actor.}</h6> */}
        </div>
      </div>
    );
  });


  return (
    <AliceCarousel
      infinite
      autoPlay
      disableButtonsControls
      disableDotsControls
      mouseTracking
      items={items}
      responsive={responsive}
    />
  );
};

export default observer(TvShowCarousel);