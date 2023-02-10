import { useHistory } from "react-router-dom";
import { unavailable } from "../../../images/DefaultImages";
import "./SingleData.css";
import MuiPlayArrowRoundedIcon from "@mui/icons-material/PlayArrowRounded";
import { styled } from "@mui/material/styles";
import { FC } from "react";
import { observer } from "mobx-react-lite";

const PlayArrowRoundedIcon = styled(MuiPlayArrowRoundedIcon)(`

  &.MuiSvgIcon-root{
    color:#abb7c4 ;
  },  &.MuiSvgIcon-root:hover {
    color: #d13131 ;
  }
  
`);

interface Props{
    tvShowId: string;
    title: string; 
    releaseYear: string;
    rating: number;
    image?: string;
}

const SingleDataTvShow: FC<Props> = (props) : JSX.Element => {
  const history = useHistory();
  var base64Flag = 'data:image/jpeg;base64,';
  var img = base64Flag + props.image;

  const handleClick = (id:string) => {
    history.push(`/tvshows/${id}`);
  };
  const setVoteClass = (vote:any) => {
    if (vote >= 8) {
      return "green";
    } else if (vote >= 6) {
      return "orange";
    } else {
      return "red";
    }
  };

  return (
    <>
      <div
        style={{ color: "white" }}
        className="SingleDataMedia"
        onClick={() => handleClick(props.tvShowId)}
      >
        <span className={` tag ${setVoteClass(props.rating)} vote__tag`}>
          {props.rating}
        </span>

        <img
          src={props.image ? img : unavailable}
          alt=""
        />
        <div className="read__more">
          <PlayArrowRoundedIcon
            style={{
              border: "2px solid #abb7c4",
              borderRadius: "50px",
              fontSize: "3.2rem",
              cursor: "pointer",
            }}
            className="play__btn"
          />
          {/* <button >Read More</button> */}
        </div>
        <div className="SingleDataDetails">
          <h6>
            {props.title}(
            {(props.releaseYear || "-----").substring(0, 4)})
          </h6>
        </div>
      </div>
    </>
  );
};

export default observer(SingleDataTvShow);