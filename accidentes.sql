PGDMP         #                {         
   accidentes    15.2    15.2 7    B           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            C           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            D           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            E           1262    24624 
   accidentes    DATABASE     ~   CREATE DATABASE accidentes WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Spanish_Mexico.1252';
    DROP DATABASE accidentes;
                postgres    false            �            1259    24625 	   accidente    TABLE     �   CREATE TABLE public.accidente (
    hora time without time zone,
    fecha date,
    id_usuario integer,
    id_ubicacion integer,
    id integer NOT NULL,
    id_oficial integer NOT NULL,
    id_consecuencia integer NOT NULL,
    id_conductor integer
);
    DROP TABLE public.accidente;
       public         heap    postgres    false            �            1259    32824    accidente_id_seq    SEQUENCE     �   CREATE SEQUENCE public.accidente_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.accidente_id_seq;
       public          postgres    false    214            F           0    0    accidente_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.accidente_id_seq OWNED BY public.accidente.id;
          public          postgres    false    225            �            1259    24631 	   conductor    TABLE     �   CREATE TABLE public.conductor (
    num_licencia integer NOT NULL,
    nombre character varying(50),
    cinturon boolean,
    edad integer,
    seguro boolean,
    estado "char"
);
    DROP TABLE public.conductor;
       public         heap    postgres    false            �            1259    24634    consecuencia    TABLE     �   CREATE TABLE public.consecuencia (
    vialidad character varying(30),
    vehiculo character varying(30),
    conductor character varying(30),
    pasajero character varying(30),
    id integer NOT NULL
);
     DROP TABLE public.consecuencia;
       public         heap    postgres    false            �            1259    41008    consecuencia_id_seq    SEQUENCE     �   CREATE SEQUENCE public.consecuencia_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.consecuencia_id_seq;
       public          postgres    false    216            G           0    0    consecuencia_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.consecuencia_id_seq OWNED BY public.consecuencia.id;
          public          postgres    false    226            �            1259    24640    oficial    TABLE     �   CREATE TABLE public.oficial (
    num_placa integer NOT NULL,
    cargo character varying(15),
    nombre character varying(50)
);
    DROP TABLE public.oficial;
       public         heap    postgres    false            �            1259    24646    pasajero    TABLE     �   CREATE TABLE public.pasajero (
    nombre character varying(50),
    cinturon boolean,
    cantidad integer,
    asiento integer,
    curp character(18) NOT NULL,
    matricula_carro character varying(7)
);
    DROP TABLE public.pasajero;
       public         heap    postgres    false            �            1259    24899 	   ubicacion    TABLE     �   CREATE TABLE public.ubicacion (
    id integer NOT NULL,
    estado character varying(50) NOT NULL,
    municipio character varying(50) NOT NULL,
    colonia character varying(50) NOT NULL,
    calle character varying(50) NOT NULL
);
    DROP TABLE public.ubicacion;
       public         heap    postgres    false            �            1259    24898    ubicacion_id_seq    SEQUENCE     �   CREATE SEQUENCE public.ubicacion_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.ubicacion_id_seq;
       public          postgres    false    224            H           0    0    ubicacion_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.ubicacion_id_seq OWNED BY public.ubicacion.id;
          public          postgres    false    223            �            1259    24733    usuario    TABLE     �   CREATE TABLE public.usuario (
    id_usuario integer NOT NULL,
    nombre character varying(50) NOT NULL,
    password character varying(50) NOT NULL,
    tipo_usuario character varying(50) NOT NULL
);
    DROP TABLE public.usuario;
       public         heap    postgres    false            �            1259    24732    usuario_id_usuario_seq    SEQUENCE     �   CREATE SEQUENCE public.usuario_id_usuario_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.usuario_id_usuario_seq;
       public          postgres    false    222            I           0    0    usuario_id_usuario_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.usuario_id_usuario_seq OWNED BY public.usuario.id_usuario;
          public          postgres    false    221            �            1259    24649    vehiculo    TABLE     �   CREATE TABLE public.vehiculo (
    matricula character(7) NOT NULL,
    modelo character varying(50),
    tipo character varying(25),
    asegurado boolean,
    num_licencia_conductor integer
);
    DROP TABLE public.vehiculo;
       public         heap    postgres    false            �            1259    24655    vehiculo_pasajero    TABLE     m   CREATE TABLE public.vehiculo_pasajero (
    "MatriculaVehiculo" integer,
    "CURPPasajero" character(18)
);
 %   DROP TABLE public.vehiculo_pasajero;
       public         heap    postgres    false            �           2604    32825    accidente id    DEFAULT     l   ALTER TABLE ONLY public.accidente ALTER COLUMN id SET DEFAULT nextval('public.accidente_id_seq'::regclass);
 ;   ALTER TABLE public.accidente ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    225    214            �           2604    41009    consecuencia id    DEFAULT     r   ALTER TABLE ONLY public.consecuencia ALTER COLUMN id SET DEFAULT nextval('public.consecuencia_id_seq'::regclass);
 >   ALTER TABLE public.consecuencia ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    226    216            �           2604    24902    ubicacion id    DEFAULT     l   ALTER TABLE ONLY public.ubicacion ALTER COLUMN id SET DEFAULT nextval('public.ubicacion_id_seq'::regclass);
 ;   ALTER TABLE public.ubicacion ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    223    224    224            �           2604    24736    usuario id_usuario    DEFAULT     x   ALTER TABLE ONLY public.usuario ALTER COLUMN id_usuario SET DEFAULT nextval('public.usuario_id_usuario_seq'::regclass);
 A   ALTER TABLE public.usuario ALTER COLUMN id_usuario DROP DEFAULT;
       public          postgres    false    221    222    222            3          0    24625 	   accidente 
   TABLE DATA           y   COPY public.accidente (hora, fecha, id_usuario, id_ubicacion, id, id_oficial, id_consecuencia, id_conductor) FROM stdin;
    public          postgres    false    214   5A       4          0    24631 	   conductor 
   TABLE DATA           Y   COPY public.conductor (num_licencia, nombre, cinturon, edad, seguro, estado) FROM stdin;
    public          postgres    false    215   �A       5          0    24634    consecuencia 
   TABLE DATA           S   COPY public.consecuencia (vialidad, vehiculo, conductor, pasajero, id) FROM stdin;
    public          postgres    false    216   TB       6          0    24640    oficial 
   TABLE DATA           ;   COPY public.oficial (num_placa, cargo, nombre) FROM stdin;
    public          postgres    false    217   �B       7          0    24646    pasajero 
   TABLE DATA           ^   COPY public.pasajero (nombre, cinturon, cantidad, asiento, curp, matricula_carro) FROM stdin;
    public          postgres    false    218   $C       =          0    24899 	   ubicacion 
   TABLE DATA           J   COPY public.ubicacion (id, estado, municipio, colonia, calle) FROM stdin;
    public          postgres    false    224   �C       ;          0    24733    usuario 
   TABLE DATA           M   COPY public.usuario (id_usuario, nombre, password, tipo_usuario) FROM stdin;
    public          postgres    false    222   BD       8          0    24649    vehiculo 
   TABLE DATA           ^   COPY public.vehiculo (matricula, modelo, tipo, asegurado, num_licencia_conductor) FROM stdin;
    public          postgres    false    219   �D       9          0    24655    vehiculo_pasajero 
   TABLE DATA           P   COPY public.vehiculo_pasajero ("MatriculaVehiculo", "CURPPasajero") FROM stdin;
    public          postgres    false    220   WE       J           0    0    accidente_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.accidente_id_seq', 11, true);
          public          postgres    false    225            K           0    0    consecuencia_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.consecuencia_id_seq', 11, true);
          public          postgres    false    226            L           0    0    ubicacion_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.ubicacion_id_seq', 14, true);
          public          postgres    false    223            M           0    0    usuario_id_usuario_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.usuario_id_usuario_seq', 16, true);
          public          postgres    false    221            �           2606    24663    conductor Conductor_pkey 
   CONSTRAINT     b   ALTER TABLE ONLY public.conductor
    ADD CONSTRAINT "Conductor_pkey" PRIMARY KEY (num_licencia);
 D   ALTER TABLE ONLY public.conductor DROP CONSTRAINT "Conductor_pkey";
       public            postgres    false    215            �           2606    24667    oficial Oficial_pkey 
   CONSTRAINT     [   ALTER TABLE ONLY public.oficial
    ADD CONSTRAINT "Oficial_pkey" PRIMARY KEY (num_placa);
 @   ALTER TABLE ONLY public.oficial DROP CONSTRAINT "Oficial_pkey";
       public            postgres    false    217            �           2606    24669    pasajero Pasajero_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.pasajero
    ADD CONSTRAINT "Pasajero_pkey" PRIMARY KEY (curp);
 B   ALTER TABLE ONLY public.pasajero DROP CONSTRAINT "Pasajero_pkey";
       public            postgres    false    218            �           2606    24671    vehiculo Vehiculo_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.vehiculo
    ADD CONSTRAINT "Vehiculo_pkey" PRIMARY KEY (matricula);
 B   ALTER TABLE ONLY public.vehiculo DROP CONSTRAINT "Vehiculo_pkey";
       public            postgres    false    219            �           2606    32827    accidente accidente_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.accidente
    ADD CONSTRAINT accidente_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.accidente DROP CONSTRAINT accidente_pkey;
       public            postgres    false    214            �           2606    41011    consecuencia consecuencia_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.consecuencia
    ADD CONSTRAINT consecuencia_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.consecuencia DROP CONSTRAINT consecuencia_pkey;
       public            postgres    false    216            �           2606    24904    ubicacion ubicacion_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.ubicacion
    ADD CONSTRAINT ubicacion_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.ubicacion DROP CONSTRAINT ubicacion_pkey;
       public            postgres    false    224            �           2606    24738    usuario usuario_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id_usuario);
 >   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_pkey;
       public            postgres    false    222            �           1259    49205    fki_fk_consecuencia_accidente    INDEX     ^   CREATE INDEX fki_fk_consecuencia_accidente ON public.accidente USING btree (id_consecuencia);
 1   DROP INDEX public.fki_fk_consecuencia_accidente;
       public            postgres    false    214            �           2606    24687 &   pasajero Pasajero_Matricula-carro_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.pasajero
    ADD CONSTRAINT "Pasajero_Matricula-carro_fkey" FOREIGN KEY (matricula_carro) REFERENCES public.vehiculo(matricula);
 R   ALTER TABLE ONLY public.pasajero DROP CONSTRAINT "Pasajero_Matricula-carro_fkey";
       public          postgres    false    219    3224    218            �           2606    24702 5   vehiculo_pasajero Vehiculo-Pasajero_CURPPasajero_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.vehiculo_pasajero
    ADD CONSTRAINT "Vehiculo-Pasajero_CURPPasajero_fkey" FOREIGN KEY ("CURPPasajero") REFERENCES public.pasajero(curp) NOT VALID;
 a   ALTER TABLE ONLY public.vehiculo_pasajero DROP CONSTRAINT "Vehiculo-Pasajero_CURPPasajero_fkey";
       public          postgres    false    3222    220    218            �           2606    24707 ,   vehiculo Vehiculo_No.Licencia-Conductor_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.vehiculo
    ADD CONSTRAINT "Vehiculo_No.Licencia-Conductor_fkey" FOREIGN KEY (num_licencia_conductor) REFERENCES public.conductor(num_licencia) NOT VALID;
 X   ALTER TABLE ONLY public.vehiculo DROP CONSTRAINT "Vehiculo_No.Licencia-Conductor_fkey";
       public          postgres    false    215    219    3216            �           2606    65584     accidente fk_accidente_conductor    FK CONSTRAINT     �   ALTER TABLE ONLY public.accidente
    ADD CONSTRAINT fk_accidente_conductor FOREIGN KEY (id_conductor) REFERENCES public.conductor(num_licencia);
 J   ALTER TABLE ONLY public.accidente DROP CONSTRAINT fk_accidente_conductor;
       public          postgres    false    215    214    3216            �           2606    32832    accidente fk_accidente_oficial    FK CONSTRAINT     �   ALTER TABLE ONLY public.accidente
    ADD CONSTRAINT fk_accidente_oficial FOREIGN KEY (id_oficial) REFERENCES public.oficial(num_placa);
 H   ALTER TABLE ONLY public.accidente DROP CONSTRAINT fk_accidente_oficial;
       public          postgres    false    214    3220    217            �           2606    49200 #   accidente fk_consecuencia_accidente    FK CONSTRAINT     �   ALTER TABLE ONLY public.accidente
    ADD CONSTRAINT fk_consecuencia_accidente FOREIGN KEY (id_consecuencia) REFERENCES public.consecuencia(id) NOT VALID;
 M   ALTER TABLE ONLY public.accidente DROP CONSTRAINT fk_consecuencia_accidente;
       public          postgres    false    214    216    3218            �           2606    24739    accidente fk_usuario_accidente    FK CONSTRAINT     �   ALTER TABLE ONLY public.accidente
    ADD CONSTRAINT fk_usuario_accidente FOREIGN KEY (id_usuario) REFERENCES public.usuario(id_usuario);
 H   ALTER TABLE ONLY public.accidente DROP CONSTRAINT fk_usuario_accidente;
       public          postgres    false    214    3226    222            �           2606    24905    accidente kf_id_ubicacion    FK CONSTRAINT     �   ALTER TABLE ONLY public.accidente
    ADD CONSTRAINT kf_id_ubicacion FOREIGN KEY (id_ubicacion) REFERENCES public.ubicacion(id);
 C   ALTER TABLE ONLY public.accidente DROP CONSTRAINT kf_id_ubicacion;
       public          postgres    false    3228    224    214            3   D   x�E��� �j�.��7<�d�9 U����}�H��T��0纔�-�Ӹ4�~+��T��}"� j&�      4   �   x�M��
�0���S�	�&�l��vO��	
[7�v��M;S(�ϗ?�R
�4ޟ@{�.XK@�s*��[���`��ì�@7%_�֐�e-%i�D��*�ViQkS�jSx��,��쳨ũE�,s�0L�"oנu���y
1�C?�\���GQ�g�S�I�{@n`'ڡ�����o;D|�Ai      5   q   x�-L9�0������/Yd�JQ ���RɁ���&o��ǰ�~v�I2�v70*�i�BD�Z�3+������e�8�9��J*c�e�PVD�j y���ZoD�I@/<      6   ?   x�37��L�LII�211��O�LN��,����2106� �8�@\cc#δb �,N�\1z\\\ d�      7   y   x�]�K
1D�է����(.���" -�-��kjUT߸��n����݆o0j��n/��+���%� F:��ng^��qZ����bV?�tLi�i�>\V�=�]�rވ��m*�      =   �   x��;�0��z�9��t��4�Mlm���񉛿�w���e��P��)�U	g��}��e��(P�(aă�\iuW{7��V����.�Z�B89�s)�;W��c�0<�1zDrOɓ�{��N"�l|3K      ;   `   x�M��
� D�w?&����ˊ>l�ƚ}E(r`3�z��9���M\�Fs8)�)�p�N��P_]5��)�5(��#��Z'H����m��u�� �s0�      8   �   x�]�1�0Eg�=J촩�@��TH�Z"U���)�_��v�*�#�q���G�C�A��N��%��8��Z���Ս2�?����ݹk�*� <sZ�6ͪ���8]�"���V��1C0�s����6�	�Ҩ�>S.�q= ��6#      9      x������ � �     