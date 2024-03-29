﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Mobiles
{
    public enum DonEnum
    {
        //Dons de classe

        //Barbare
        DonSupClasse = 0,
        RageBerserker,
        EsquiveInstinctive, //+Esquive instinctive supérieur (rang2)
        SensPieges,
        ReductionDegat,
        RageGrandBerserker,
        VolonteIndomptable,
        RageSansFatigue,
        RageMaitreBerserker,
        //Barde
        ContreChant,
        InspirationVaillante,
        InspirationTalent,
        InspirationHero,
        ChantLiberte,
        InspirationIntrepide,
        //Druide
        CompagnonAnimal,

        //Mage Nubien
        MageIntel,
        MageSagesse,
        MageCharisme,

        //Moine
        DelugeCoup,
        EsquiveTotale,
        Serenite,
        FrappeKi,
        PuretePhysique,
        PlenitudePhysique,
        CorpDiamant,
        SautAmeliore,
        AmeDiamant,
        JeunesseEternelle,
        PerfectionEtre,
        //Roublard
        AttaqueSournoise,
        RecherchePiege,

        //RODEUR
        EmpathieSauvage,

        EnemiJureAberration,
        EnemiJureGeant,
        EnemiJureAnimal,
        EnemiJureHumanoide,
        EnemiJureCreatureArticifielle,
        EnemiJureMagique,
        EnemiJureDragon,
        EnemiJureElementaire,
        EnemiJureMortVivant,
        EnemiJurePlante,
        EnemiJureVermine,

        //Dons généraux
        AffiniteMagique = 1000,
        AmeliorationDesCreaturesConvoquees,
      //  ArmeDePredilection,
        ArmeDePredilectionBaton,
        ArmeDePredilectionMasse,
        ArmeDePredilectionEpee,
        ArmeDePredilectionHache,
        ArmeDePredilectionLance,
        ArmeDePredilectionHast,
        ArmeDePredilectionDague,
        ArmeDePredilectionArc,
        ArmeDePredilectionArbalete,
        ArmeDePredilectionJet,
        ArmeDePredilectionPoing,

        //ArmeDePredilectionSuperieure,
        ArmeDePredilectionSupBaton,
        ArmeDePredilectionSupMasse,
        ArmeDePredilectionSupEpee,
        ArmeDePredilectionSupHache,
        ArmeDePredilectionSupLance,
        ArmeDePredilectionSupHast,
        ArmeDePredilectionSupDague,
        ArmeDePredilectionSupArc,
        ArmeDePredilectionSupArbalete,
        ArmeDePredilectionSupJet,
        ArmeDePredilectionSupPoing,


     //   ArmeEnMain, Pour déguainer... ch'bof

        Athletisme,
     //   AttaqueAuGalop,
        AttaqueEclair,
        AttaqueEnFinesse,
        AttaqueEnPuissance,
        AttaqueEnRotation,
        AttaquesReflexes,
        Autonome,
      //  ChargeDevastatrice,
      //  CombatADeuxArmes,
        CombatEnAveugle,
        CombatMonte,
        CoupEtourdissant,
        Course,
        DefenseADeuxArmes,
        Discret,
        DispenseDeComposantsMateriels,
        DoigtsDeFee,
        DurACuire,
       // EcoleRenforcee,
        EcoleRenforceAbjuration,
        EcoleRenforceDivination,
        EcoleRenforceEnchantement,
        EcoleRenforceEvocation,
        EcoleRenforceIllusion,
        EcoleRenforceInvocation,
        EcoleRenforceNecromancie,
        EcoleRenforceTransmutation,

   //     EcoleSuperieure,
        EcoleSuperieureAbjuration,
        EcoleSuperieureDivination,
        EcoleSuperieureEnchantement,
        EcoleSuperieureEvocation,
        EcoleSuperieureIllusion,
        EcoleSuperieureInvocation,
        EcoleSuperieureNecromancie,
        EcoleSuperieureTransmutation,

        EfficaciteDesSortsAccrue,
        EfficaciteDesSortsSuperieure,
        EmpriseSurLesMortsVivants,
       // Enchainement,
        Endurance,
        Esquive,
        PositionDefensiveAmelio,
        FeuNourri,
        FinLimier,
        Fourberie,
        FraterniteAnimale,
        Funambule,
        IncantationAnimale,
        InterceptionDeProjectile,
        MagieDeGuerre,
     // MaitriseDesSorts,
     // MaitriseDuCombatADeuxArmes,
     // ManiementUneArmeDeGuerre,
     // ManiementUneArmeExotique,
     // ManiementDesArmesCourantes,
     // ManiementDesBoucliers,
     // ManiementDuPavois,
        Meticuleux,
        Negociation,
        ParadeDeProjectiles,
        Persuasion,
     // Pietinement,
        Pistage,
      //  PortDesArmuresIntermediaires,
     //   PortDesArmuresLegeres,
     //   PortDesArmuresLourdes,
    //    Prestige,
     //   RechargementRapide,
        ReflexesSurhumains,
        Robustesse,
        SavoirFaireMecanique,
     //   ScienceDeLaDestruction,
     //   ScienceDeInitiative,
        ScienceDeLaBousculade,
        ScienceDeLaFeinte,
      //  ScienceDeLaLutte,
     //   ScienceDuCombatADeuxArmes,
        ScienceDuCombatAMainsNues,
        ScienceDuContresort,
        ScienceDuCoupDeBouclier,
      //  ScienceDuCritique,
        ScienceDuCritiqueBaton,
        ScienceDuCritiqueMasse,
        ScienceDuCritiqueEpee,
        ScienceDuCritiqueHache,
        ScienceDuCritiqueLance,
        ScienceDuCritiqueHast,
        ScienceDuCritiqueDague,
        ScienceDuCritiqueArc,
        ScienceDuCritiqueArbalete,
        ScienceDuCritiqueJet,
        ScienceDuCritiquePoing,

        ScienceDuCritiqueSupBaton,
        ScienceDuCritiqueSupMasse,
        ScienceDuCritiqueSupEpee,
        ScienceDuCritiqueSupHache,
        ScienceDuCritiqueSupLance,
        ScienceDuCritiqueSupHast,
        ScienceDuCritiqueSupDague,
        ScienceDuCritiqueSupArc,
        ScienceDuCritiqueSupArbalete,
        ScienceDuCritiqueSupJet,
        ScienceDuCritiqueSupPoing,

        ScienceDuCrocEnJambe,
        ScienceDuDesarmement,
        ScienceDuRenversement,
        ScienceDuRenvoi,
        ScienceDuTirDePrecision,
        SouplesseDuSerpent,
     //   SpecialisationMartiale,
        SpecialisationMartialeBaton,
        SpecialisationMartialeMasse,
        SpecialisationMartialeEpee,
        SpecialisationMartialeHache,
        SpecialisationMartialeLance,
        SpecialisationMartialeHast,
        SpecialisationMartialeDague,
        SpecialisationMartialeArc,
        SpecialisationMartialeArbalete,
        SpecialisationMartialeJet,
        SpecialisationMartialePoing,
      
      //  SpecialisationMartialeSuperieure,
        SpecialisationMartialeSupBaton,
        SpecialisationMartialeSupMasse,
        SpecialisationMartialeSupEpee,
        SpecialisationMartialeSupHache,
        SpecialisationMartialeSupLance,
        SpecialisationMartialeSupHast,
        SpecialisationMartialeSupDague,
        SpecialisationMartialeSupArc,
        SpecialisationMartialeSupArbalete,
        SpecialisationMartialeSupJet,
        SpecialisationMartialeSupPoing,

        SuccessionEnchainements,
      //  Talent,
        TalentAcrobaties,
        TalentArtMagie,
        TalentCouture,
        TalentForge,
        TalentIngenierie,
        TalentEbenisterie,
        TalentChimie,
        TalentAgriculture,
        TalentMinage,
        TalentBuchage,
        TalentBluff,
        TalentConcentration,
        TalentErudition,
        TalentCrochetage,
        TalentDeguisement,
        TalentDeplacementSilentieux,
        TalentDesamorcage,
        TalentDetection,
        TalentDiplomatie,
        TalentDiscretion,
        TalentDressage,
        TalentEquilibre,
        TalentEquitation,
        TalentEscalade,
        TalentEscamotage,
        TalentEstimation,
        TalentEvasion,
        TalentFouille,
        TalentIntimidation,
        TalentVoleVoile,
        TalentPerceptionAuditive,
        TalentPremiersSecours,
        TalentPsychologie,
        TalentRenseignements,
        TalentRepresentation,
        TalentSaut,
        TalentSurvie,
        TalentUtilisationObjetsMagiques,
        TalentCuisine,
        //TalentPeche,
        TalentBrassage,
        TalentChirurgie,

        TirABoutPortant,
        TirDeLoin,
        TirDePrecision,
        TirEnMouvement,
        TirMonte,
        TirRapide,
        Vigilance,
        VigueurSurhumaine,
        VolonteDeFer,
        Voltigeur,

    

        Maximum
    }
}
