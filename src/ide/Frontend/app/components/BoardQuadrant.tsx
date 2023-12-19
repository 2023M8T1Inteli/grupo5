'use client'
import Image from "next/image";
import { useEffect, useState } from "react";

export default function BoardQuadrant({icon, active} : {icon : any, active?: boolean}) {

	const [isActive, setIsActive] = useState(false)

	useEffect(() => {
		if(active) {
			setIsActive(true)
		} else {
			setIsActive(false)
		}
	}, [active])

	const [opacity, setOpacity] = useState('opacity-20')

	useEffect(() => {
		if(isActive) {
			setOpacity('opacity-100')
		} else {
			setOpacity('opacity-20')
		}
	}, [isActive])

	return (
		<div className={opacity + ' ' + 'cursor-pointer'}>
			<Image src={icon} alt='Board' width={90} height={90}/>
		</div>
	)
}