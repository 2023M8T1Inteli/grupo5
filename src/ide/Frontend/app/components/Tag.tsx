export interface ITag {
	text: string;
	color: string;
	bgColor: string;
}

export default function Tag(props: {bgColor: string, color: string, text: string}) {
	return (
		<div className={`flex justify-center items-center w-fit bg-[${props.bgColor}] color-[${props.color}]`}>
			{props.text}
		</div>
	)
}