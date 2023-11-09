export default function TableItem(props : {children: React.ReactNode, className: string}) {
	return (
		<div className={'text-xl font-normal text-[#667085]' +  ' ' + props.className}>{props.children}</div>
	)
}