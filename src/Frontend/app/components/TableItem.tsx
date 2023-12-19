export default function TableItem(props : {children: React.ReactNode, className: string}) {
	return (
		<div suppressHydrationWarning className={'flex items-center text-xl font-normal text-[#667085] h-full' +  ' ' + props.className}>{props.children}</div>
	)
}