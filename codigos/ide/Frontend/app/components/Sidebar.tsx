import Image from "next/image"
import Logo from '../../public/logo.svg'
import Home from '../../public/home.svg'
import Chart from '../../public/chart.svg'
import Profile from '../../public/profile.svg'
import Stethoscope from '../../public/stethoscope.svg'
import Heart from '../../public/heart.svg'
import Calendar from '../../public/calendar.svg'
import MenuItem from "../components/MenuItem"
import LogoutButton from "../components/LogoutButton"

const menuItems = [
  {icon: Home, text: 'Página inicial', href: '/dashboard'},
  // {icon: Chart, text: 'Métricas', href: '/dashboard/metrics'},
  {icon: Profile, text: 'Pacientes', href: '/dashboard/patients'},
  {icon: Stethoscope, text: 'Terapeutas', href: '/dashboard/therapists'},
  {icon: Heart, text: 'Terapias', href: '/dashboard/therapies'},
  {icon: Calendar, text: 'Agenda', href: '/dashboard/calendar'},
];

export interface SidebarProps {
}

export default function Sidebar() {
  return (
    <aside className='flex flex-col justify-between w-[15%] border-r border-[#EFEFEF] h-[100vh] p-6'>
      <div className='flex flex-col gap-11'>
        <div className='flex gap-3 items-center'>
          <Image src={Logo} alt='Logotipo' width={36} height={36} className="m-3"/>
          <span className='text-[#09090A] max-w-[7.2rem] text-base'>Portal do terapeuta</span>
        </div>
        <div className='flex flex-col gap-6 '>
          {menuItems.map((item, index) => (
            <MenuItem key={index} icon={item.icon} text={item.text} href={item.href}/>
          ))}
        </div>
      </div>
      <LogoutButton/>
    </aside>
  );
}